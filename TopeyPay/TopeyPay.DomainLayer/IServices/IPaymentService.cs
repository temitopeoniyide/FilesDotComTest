using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopeyPay.Domain.Models;

namespace TopeyPay.Domain.IServices
{
   public interface IPaymentService
    {
       Task<Payment> AddPayment(Payment info);
    }
}
