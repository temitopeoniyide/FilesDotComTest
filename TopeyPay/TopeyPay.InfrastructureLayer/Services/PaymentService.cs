using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopeyPay.Domain;
using TopeyPay.Domain.IServices;
using TopeyPay.Domain.Models;
using TopeyPay.Shared.Contracts;

namespace TopeyPay.Infrastructure.Services
{
    public class PaymentService:IPaymentService
    {

        private readonly IUnitOfWork _unitofwork;
      //  private IConfiguration config;
        private readonly ILogWriter _logWriter;
        public PaymentService(IUnitOfWork unitofwork,  ILogWriter logWriter)
        {
            _unitofwork = unitofwork;  
            _logWriter = logWriter;
        }
        public async Task<Payment> AddPayment(Payment Info)
        {
            try
            {
                Info.PaymentStatus.Status = "pending";
              var payment= await _unitofwork.Payments.AddAsync(Info);
                await _unitofwork.CommitChangesAsync();
                return payment;
            }
            catch(Exception ex)
            {
                _logWriter.LogWrite("Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException + Environment.NewLine + "StackTrace: " + ex.StackTrace);
                return null;
            }
        }
    }
}
