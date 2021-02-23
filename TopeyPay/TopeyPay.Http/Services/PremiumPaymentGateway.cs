using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopeyPay.Http.Http;
using TopeyPay.Http.Interfaces;
using TopeyPay.Shared.Contracts;

namespace TopeyPay.Http.Services
{
    public class PremiumPaymentGateway :IPremiumPaymentGateway
    {
        private readonly ILogWriter _logWriter;
        public PremiumPaymentGateway(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }
        public async Task<bool> MakePayment(string url, object payload) => await new HttpClientService(_logWriter).CallService(url, payload);

    }
}
