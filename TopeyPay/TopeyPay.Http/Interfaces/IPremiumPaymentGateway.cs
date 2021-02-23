using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopeyPay.Http.Interfaces
{
    public interface IPremiumPaymentGateway
    {
        Task<bool> MakePayment(string url, object payload);
    }
}
