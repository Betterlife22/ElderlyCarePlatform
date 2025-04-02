using DAL.Libraries;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPaymentRepository
    {
        string CreatePaymentUrl(PaymentInformationDTO model);
        PaymentResponseDTO PaymentExecute(IQueryCollection collections);
    }
}
