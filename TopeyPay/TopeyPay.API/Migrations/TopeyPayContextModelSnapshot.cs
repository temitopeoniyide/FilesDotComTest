﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopeyPay.Infrastructure;

namespace TopeyPay.Migrations
{
    [DbContext(typeof(TopeyPayContext))]
    partial class TopeyPayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("TopeyPay.Domain.Models.Payment", b =>
                {
                    b.Property<long>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CardHolder")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("PaymentId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("TopeyPay.Domain.Models.PaymentStatus", b =>
                {
                    b.Property<Guid>("PaymentStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("PaymentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("PaymentStatusId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.ToTable("PaymentStatus");
                });

            modelBuilder.Entity("TopeyPay.Domain.Models.PaymentStatus", b =>
                {
                    b.HasOne("TopeyPay.Domain.Models.Payment", "Payment")
                        .WithOne("PaymentStatus")
                        .HasForeignKey("TopeyPay.Domain.Models.PaymentStatus", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("TopeyPay.Domain.Models.Payment", b =>
                {
                    b.Navigation("PaymentStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
