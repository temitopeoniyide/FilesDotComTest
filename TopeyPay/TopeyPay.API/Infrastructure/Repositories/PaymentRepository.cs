using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Domain.IRepository;
using TopeyPay.Domain.Models;

namespace TopeyPay.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(TopeyPayContext context) : base(context)
        {
        }



        public TopeyPayContext DataContext { get { return Context as TopeyPayContext; } }
    }
}
