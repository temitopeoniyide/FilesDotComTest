using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopeyPay.Http.Http;
using TopeyPay.Http.Interfaces;
using TopeyPay.Shared.Contracts;

namespace TopeyPay.Http.Services
{
   public class CheapPaymentGateway:ICheapPaymentGateway
    {
        private readonly ILogWriter _logWriter;
        public CheapPaymentGateway(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }
        public async Task<bool> MakePayment(string url, object payload) =>await new HttpClientService(_logWriter).CallService(url,payload);

    }
}
