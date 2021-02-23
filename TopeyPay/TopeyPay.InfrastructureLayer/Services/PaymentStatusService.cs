using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopeyPay.Domain;
using TopeyPay.Domain.IServices;
using TopeyPay.Shared.Contracts;

namespace TopeyPay.Infrastructure.Services
{
   public class PaymentStatusService:IPaymentStatusService
    {
        private readonly IUnitOfWork _unitofwork;
        //  private IConfiguration config;
        private readonly ILogWriter _logWriter;
        public PaymentStatusService(IUnitOfWork unitofwork, ILogWriter logWriter)
        {
            _unitofwork = unitofwork;
            _logWriter = logWriter;
        }
        public async Task<int> UpdatePaymentStatus(long PaymentId, string status)
        {
            try
            {
               var payInfo= await _unitofwork.PaymentStatus.FirstOrDefaultAsync(o => o.PaymentId == PaymentId);
                payInfo.Status = status;
                _unitofwork.PaymentStatus.Update(payInfo);
              return await  _unitofwork.CommitChangesAsync();
            }
            catch(Exception ex)
            {
                _logWriter.LogWrite("Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException + Environment.NewLine + "StackTrace: " + ex.StackTrace);

                return 0;
            }
        }
       
    }
}
