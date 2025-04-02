using AutoMapper;
using BLL.DTO.BookingDTOs;
using BLL.DTO.ReceiptDTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages.Booking
{
    public class BookingListModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IReceiptService _receiptService;
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        public BookingListModel(IBookingService bookingService, IUnitOfWork unitOfWork, IReceiptService receiptService,IPaymentService paymentService)
        {
            _bookingService = bookingService;
            _receiptService = receiptService;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        [BindProperty]
        public List<BookingDTO> Bookings { get; set; }
        
        public async Task<ActionResult> OnGetAsync()
        {
            Bookings = await _bookingService.GetAllBookingsAsync();
            return Page();
        }
        //public async Task<ActionResult> OnPostMakePaymentAsync(int bookingId)
        //{
        //    var booking = await _bookingService.GetBookingByIdAsync(bookingId);
        //    var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(booking.UserId);
        //    ReceiptCreateDTO receiptCreateDTO = new ReceiptCreateDTO()
        //    {
        //        UserId = booking.UserId,
        //        BookingId = bookingId,
        //        Ammount = booking.Total,
        //        PaymentMethod = "VnPay",
        //        Status = "Processing"
        //    };
        //    var reccepit = await _receiptService.AddReceiptAsync(receiptCreateDTO);

        //    var PaymentInfo = new PaymentInformationDTO
        //    {
        //        Name = user.UserName,
        //        ReceiptId = reccepit.Id,
        //        Amount = reccepit.Ammount,//Service price must > 5000
        //        OrderType = "VnPay",
        //        OrderDescription = $"Payment for {booking.ServiceName}"
        //    };
        //    var paymenturl = await _paymentService.VnPayPayment(PaymentInfo);
        //    return Redirect(paymenturl);
        //}
    }
}
