using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Domain;
using TopeyPay.Domain.IRepository;
using TopeyPay.Infrastructure.Repositories;

namespace TopeyPay.Infrastructure
{
    public class UnitOfWork:IUnitOfWork
    {
        public IPaymentRepository Payments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPaymentStatusRepository PaymentStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private readonly TopeyPayContext _context;

        public UnitOfWork(TopeyPayContext context)
        {
            _context = context;
            Payments = new PaymentRepository(_context);
            PaymentStatus = new PaymentStatusRepository(_context);

        }
        public async Task<int> CommitChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int CommitChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();

            //throw new NotImplementedException();
        }
    }
}
