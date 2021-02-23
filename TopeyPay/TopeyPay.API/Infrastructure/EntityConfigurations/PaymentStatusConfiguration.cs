using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Domain.Models;

namespace TopeyPay.Infrastructure.EntityConfigurations
{
    public class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.HasKey(e => e.PaymentStatusId);
            builder.Property(e => e.PaymentStatusId).ValueGeneratedOnAdd();
            builder.Property(e => e.PaymentId);
            builder.Property(e => e.Status).IsRequired().HasMaxLength(20).IsUnicode(false);

        }
    }
}

