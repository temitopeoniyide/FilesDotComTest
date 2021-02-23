using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopeyPay.Domain.IServices
{
    public interface IPaymentStatusService
    {
        Task<int> UpdatePaymentStatus(long PaymentId, string status);
    }
}
