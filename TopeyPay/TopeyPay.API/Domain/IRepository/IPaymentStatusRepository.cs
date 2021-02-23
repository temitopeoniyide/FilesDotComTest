using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Domain.Models;

namespace TopeyPay.Domain.IRepository
{
    public interface IPaymentStatusRepository:IRepository<PaymentStatus>
    {
    }
}
