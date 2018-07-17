using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.Configurations
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.UserId, x.CreditCardId, x.BankAccountId }).IsUnique();

            builder.HasOne(e => e.User)
                .WithMany(x => x.PaymentMethods)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Creditcard)
                .WithOne(x => x.PaymentMethod)
                .HasForeignKey<CreditCard>(x => x.CreditCardId);

            builder.HasOne(x => x.BankAccount)
                .WithOne(x => x.PaymentMethod)
                .HasForeignKey<BankAccount>(x => x.BankAccountId);
        }
    }
}
