using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.DTOs;
using TopeyPay.Responses;

namespace TopeyPay.Contracts
{
  public   interface IProcessPayment
    {
        Task<ProcessPaymentResponse> ProcessNewPayment(PaymentDTO Payment);
    }
}
