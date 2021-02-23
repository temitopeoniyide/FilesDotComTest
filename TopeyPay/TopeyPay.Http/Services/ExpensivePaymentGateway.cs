using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopeyPay.Http.Http;
using TopeyPay.Http.Interfaces;
using TopeyPay.Shared.Contracts;

namespace TopeyPay.Http.Services
{
   public class ExpensivePaymentGateway: IExpensivePaymentGateway
    {
        private readonly ILogWriter _logWriter;
        public ExpensivePaymentGateway(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }
        public async Task<bool> MakePayment(string url, object payload) => await new HttpClientService(_logWriter).CallService(url, payload);

    }
}
