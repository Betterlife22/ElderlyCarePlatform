using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Libraries
{
    public class PaymentInformationDTO
    {
        public string OrderType { get; set; }
        public int ReceiptId { get; set; }
        public float Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
    }
}
