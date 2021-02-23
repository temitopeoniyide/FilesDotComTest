using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Contracts;
using TopeyPay.Domain.IServices;
using TopeyPay.Domain.Models;
using TopeyPay.DTOs;
using TopeyPay.Http.Interfaces;
using TopeyPay.Responses;
using TopeyPay.Shared.Contracts;
using TopeyPay.Utils;

namespace TopeyPay.Services
{
    public class ProcessPayment:IProcessPayment
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaymentStatusService _paymentStatusService;
        private readonly ILogWriter _logWriter;
        private readonly IMapper _mapper;
        private readonly ICheapPaymentGateway _cheapPayment;
        private readonly IExpensivePaymentGateway _expensivePayment;
        private readonly IPremiumPaymentGateway _premium;
        private readonly IConfiguration _config;
        public ProcessPayment(IPaymentService paymentService, IPaymentStatusService paymentStatusService, ILogWriter logWriter, IMapper mapper, ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway,  IPremiumPaymentGateway premiumPaymentGateway, IConfiguration config)
        {
          
            _paymentService = paymentService;
            _paymentStatusService = paymentStatusService;
            _logWriter = logWriter;
            _mapper = mapper;
            _cheapPayment = cheapPaymentGateway;
            _expensivePayment = expensivePaymentGateway;
            _premium = premiumPaymentGateway;
            _config = config;
        }

        public async Task<ProcessPaymentResponse> ProcessNewPayment(PaymentDTO payment)
        {
            try
            {
                //validate request
                // 1. validate card number by calling the ValidateCardNumberClass
                if (ValidateCardNumber.IsCardNumberValid(ValidateCardNumber.RemoveSpaceAndAlphaInCardNumber(payment.CreditCardNumber))) return new ProcessPaymentResponse { Status = false, Message = "Invalid card number",ResponseType=ResponseType.badRequest };
                if (string.IsNullOrEmpty(payment.CardHolder)) return new ProcessPaymentResponse { Status = false, Message = "Provide card holder name", ResponseType = ResponseType.badRequest };
                if (payment.Amount <= 0) return new ProcessPaymentResponse { Status = false, Message = "Provide a positive amount", ResponseType = ResponseType.badRequest };
                if (ValidateCardNumber.RemoveSpaceAndAlphaInCardNumber(payment.SecurityCode).Length != 3) return new ProcessPaymentResponse { Status = false, Message = "Provide a valid security code", ResponseType = ResponseType.badRequest };
                if (DateTime.Now.Date > payment.ExpirationDate.Date) return new ProcessPaymentResponse { Status = false, Message = "Card has expired", ResponseType = ResponseType.badRequest };

                var submitPaymentInfo = await _paymentService.AddPayment(_mapper.Map<Payment>(payment));
                if (submitPaymentInfo != null)
                {
                    var response = false;
                    // Call the  -ICheapPaymentGateway(Amount <20)

                    if (payment.Amount < 20)
                        response = await _cheapPayment.MakePayment(_config.GetSection("CheapServiceURL").Value, payment);
                    //Call the - IExpensivePaymentGateway(Amount btw 21-500)
                    else if (payment.Amount > 20 && payment.Amount < 500)
                    {
                        response = await _expensivePayment.MakePayment(_config.GetSection("ExpensiveServiceURL").Value, payment);
                        //if failed retry nce with ICheapPaymentGateway
                        if (!response) response = await _cheapPayment.MakePayment(_config.GetSection("CheapServiceURL").Value, payment);
                    }
                    else
                    {
                        //Call the  PremiumPaymentService (Amount >500 ) and retry 3 times if failed
                        var retryCount = 0;
                        while (!response && retryCount < 3)
                        {

                            response = await _cheapPayment.MakePayment(_config.GetSection("PremiumServiceURL").Value, payment);
                        }

                    }

                    if (response)
                    {
                        await _paymentStatusService.UpdatePaymentStatus(submitPaymentInfo.PaymentId, "processed");
                        return new ProcessPaymentResponse { Status = response, Message = "processed", ResponseType = ResponseType.badRequest };
                    }
                    else
                    {
                        await _paymentStatusService.UpdatePaymentStatus(submitPaymentInfo.PaymentId, "processed");
                        return new ProcessPaymentResponse { Status = response, Message = "failed", ResponseType = ResponseType.badRequest };
                    }
                
                }

                else {
                    
                    return new ProcessPaymentResponse { Message = "Internal server error", Status = false, ResponseType = ResponseType.internalServerError };
                
                }
            }
            catch(Exception ex)
            {

                return new ProcessPaymentResponse { Message = "Internal server error", Status = false, ResponseType = ResponseType.internalServerError };
            }
        }
    }
}
