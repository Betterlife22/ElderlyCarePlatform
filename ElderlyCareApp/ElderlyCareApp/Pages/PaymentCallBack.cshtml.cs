using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages
{
    public class PaymentCallBackModel : PageModel
    {
        private readonly IPaymentService _paymentService;
        private readonly IReceiptService _receiptService;
        public PaymentCallBackModel(IPaymentService paymentService, IReceiptService receiptService)
        {
             _paymentService = paymentService;
            _receiptService = receiptService;
        }
        public async Task <ActionResult> OnGetAsync()
        {
            try
            {
                var response = _paymentService.PaymentExecute(Request.Query);
                await _receiptService.UpdateReceiptStatus(Int32.Parse(response.OrderId));
                return RedirectToPage("./SuccessfullPay");
            }
            catch 
            {
                return RedirectToPage("./FailPay");
            }
          

        }
    }
}
