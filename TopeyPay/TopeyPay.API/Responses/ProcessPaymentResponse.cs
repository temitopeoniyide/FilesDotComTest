using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopeyPay.Responses
{
    public class ProcessPaymentResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public  ResponseType  ResponseType{ get;set; }
    }
    public enum ResponseType
    {
        ok=1,
        badRequest,
        internalServerError,

    }
}
