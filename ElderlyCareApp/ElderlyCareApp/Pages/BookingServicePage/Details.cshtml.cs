﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using BLL.DTO.BookingDTOs;
using DAL.Common;
using BLL.DTO.ReceiptDTOs;
using BLL.Services;
using DAL.Entities;
using DAL.Libraries;
using DAL.Interfaces;
using BLL.DTO.RatingDTOs;

namespace ElderlyCareApp.Pages.BookingServicePage
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IReceiptService _receiptService;
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRatingService _ratingService;
        public DetailsModel(IBookingService bookingService, IReceiptService receiptService,IPaymentService paymentService, IUnitOfWork unitOfWork , IRatingService ratingService)
        {
            _unitOfWork = unitOfWork;
            _bookingService = bookingService;
            _receiptService = receiptService;
            _paymentService = paymentService;
            _ratingService = ratingService;
        }

        public BookingDTO Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var booking = await _bookingService.GetBookingByIdAsync(id);

            Booking = booking;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
                       
            await _bookingService.UpdateStatusBooking(id);

            return RedirectToPage("/Booking/BookingList");
        }
        public async Task<ActionResult> OnPostMakePaymentAsync(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(booking.UserId);
            ReceiptCreateDTO receiptCreateDTO = new ReceiptCreateDTO()
            {
                UserId = booking.UserId,
                BookingId = bookingId,
                Ammount = booking.Total*1000,
                PaymentMethod = "VnPay",
                Status = "Processing"
            };
            var reccepit = await _receiptService.AddReceiptAsync(receiptCreateDTO);

            var PaymentInfo = new PaymentInformationDTO
            {
                Name = user.UserName,
                ReceiptId = reccepit.Id,
                Amount = reccepit.Ammount,//Service price must > 5000
                OrderType = "VnPay",
                OrderDescription = $"Payment for {booking.ServiceName}"
            };
            var paymenturl = await _paymentService.VnPayPayment(PaymentInfo);
            return Redirect(paymenturl);
        }
        public async Task<IActionResult> OnPostSubmitFeedbackAsync(int id,int ratingScore, string review)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);

            Booking = booking;
            RatingCreateDTO ratingDto = new RatingCreateDTO
            {
                BookingId = Booking.Id,
                UserId = Booking.UserId,
                CaregiverId = Booking.CaregiverId,
                RatingScore = ratingScore,
                Review = review
            };

            await _ratingService.AddRatingAsync(ratingDto);
            TempData["FeedbackSuccess"] = "Thank you for your feedback!";
            return Page();
        }
    }
}
