using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Domain.Models;

namespace TopeyPay.Infrastructure.EntityConfigurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(e => e.PaymentId);
            builder.Property(e => e.PaymentId).ValueGeneratedOnAdd();
            builder.Property(e => e.CreditCardNumber).IsUnicode(false).HasMaxLength(40);
            builder.Property(e => e.CardHolder).IsUnicode(false).HasMaxLength(200);
            builder.Property(e => e.SecurityCode).IsUnicode(false).HasMaxLength(3);
            builder.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            builder.Property(e => e.ExpirationDate).HasColumnType("datetime");

        }
    }
}
