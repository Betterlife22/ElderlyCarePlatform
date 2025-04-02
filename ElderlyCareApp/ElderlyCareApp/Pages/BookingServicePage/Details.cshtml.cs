using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using BLL.DTO.BookingDTOs;
using DAL.Common;
using BLL.DTO.ReceiptDTOs;
using BLL.Services;
using DAL.Entities;
using DAL.Libraries;
using DAL.Interfaces;

namespace ElderlyCareApp.Pages.BookingServicePage
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IReceiptService _receiptService;
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IBookingService bookingService, IReceiptService receiptService,IPaymentService paymentService, IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
            _bookingService = bookingService;
            _receiptService = receiptService;
            _paymentService = paymentService;
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
    }
}
