using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Contracts;
using TopeyPay.DTOs;

namespace TopeyPay.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IProcessPayment _processPayment;
        public PaymentController(IProcessPayment processPayment)
        {
            _processPayment = processPayment;
        }
        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment(PaymentDTO data)
        {
            var processPaymentResponse = await _processPayment.ProcessNewPayment(data);
            if ((int)processPaymentResponse.ResponseType == 1)
                return Ok(processPaymentResponse.Message);
            else if ((int)processPaymentResponse.ResponseType == 2)
                return BadRequest(processPaymentResponse.Message);
            else
                return StatusCode(500, processPaymentResponse.Message);
                    
        }
        
    }
}
