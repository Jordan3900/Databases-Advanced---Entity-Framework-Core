﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P01_BillsPaymentSystem.Data;

namespace P01_BillsPaymentSystem.Data.Migrations
{
    [DbContext(typeof(BillsPaymentContext))]
    [Migration("20180713080732_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.BankAccount", b =>
                {
                    b.Property<int>("BankAccountId");

                    b.Property<decimal>("Balance");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("SwiftCode")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("BankAccountId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.CreditCard", b =>
                {
                    b.Property<int>("CreditCardId");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<decimal>("Limit");

                    b.Property<decimal>("MoneyOwed");

                    b.HasKey("CreditCardId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankAccountId");

                    b.Property<int>("CreditCardId");

                    b.Property<int>("Type");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "CreditCardId", "BankAccountId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL AND [BankAccountId] IS NOT NULL");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.BankAccount", b =>
                {
                    b.HasOne("P01_BillsPaymentSystem.Data.Models.PaymentMethod", "PaymentMethod")
                        .WithOne("BankAccount")
                        .HasForeignKey("P01_BillsPaymentSystem.Data.Models.BankAccount", "BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.CreditCard", b =>
                {
                    b.HasOne("P01_BillsPaymentSystem.Data.Models.PaymentMethod", "PaymentMethod")
                        .WithOne("Creditcard")
                        .HasForeignKey("P01_BillsPaymentSystem.Data.Models.CreditCard", "CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.PaymentMethod", b =>
                {
                    b.HasOne("P01_BillsPaymentSystem.Data.Models.User", "User")
                        .WithMany("PaymentMethods")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}