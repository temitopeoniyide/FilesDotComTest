using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Domain.Models;
using TopeyPay.Infrastructure.EntityConfigurations;

namespace TopeyPay.Infrastructure
{
    public class TopeyPayContext:DbContext
    {
       
            public TopeyPayContext(DbContextOptions<TopeyPayContext> options) : base(options)
            {

            }
            public DbSet<Payment> Payment {get; set;}
            public DbSet<PaymentStatus> PaymentStatus { get; set; }
            protected override void OnModelCreating(ModelBuilder builder)
            {
               builder.ApplyConfiguration(new PaymentConfiguration());
               builder.ApplyConfiguration(new PaymentStatusConfiguration());

            }
        
    }
}
