using DAL.Libraries;
using DAL.Repo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository= paymentRepository;
        }
        public Task<string> VnPayPayment(PaymentInformationDTO paymentInformationDTO)
        {
            var paymenturl = _paymentRepository.CreatePaymentUrl(paymentInformationDTO);
            return Task.FromResult( paymenturl );
        }
        public PaymentResponseDTO PaymentExecute(IQueryCollection collections)
        {
            return _paymentRepository.PaymentExecute(collections);
        }
    }
}
