using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopeyPay.Domain.Models
{
    public class PaymentStatus
    {
        public Guid PaymentStatusId { get; set; }
        public long PaymentId { get; set; }
        public string Status { get; set; }
        public Payment Payment { get; set; }
    }
}
