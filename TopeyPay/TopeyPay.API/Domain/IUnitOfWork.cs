using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Domain.IRepository;

namespace TopeyPay.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IPaymentRepository Payments { get; set; }
        IPaymentStatusRepository PaymentStatus { get; set; }
     

        int CommitChanges();
        Task<int> CommitChangesAsync();

    }
}

