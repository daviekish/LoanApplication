﻿// <auto-generated />
using System;
using DomainLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DomainLayer.Migrations
{
    [DbContext(typeof(LoanApplicationDBContext))]
    [Migration("20230725134735_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DomainLayer.LoanScheduleViewModel", b =>
                {
                    b.Property<string>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Installment")
                        .HasColumnType("float");

                    b.Property<double>("Interest")
                        .HasColumnType("float");

                    b.Property<double>("LoanAmount")
                        .HasColumnType("float");

                    b.Property<double>("LoanBalance")
                        .HasColumnType("float");

                    b.Property<double>("LoanPeriod")
                        .HasColumnType("float");

                    b.Property<double>("LoanType")
                        .HasColumnType("float");

                    b.Property<Guid?>("LoansId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentFreq")
                        .HasColumnType("int");

                    b.Property<double>("PrincipalAmount")
                        .HasColumnType("float");

                    b.HasKey("LoanId");

                    b.HasIndex("LoansId");

                    b.ToTable("LoanScheduleViewModel");
                });

            modelBuilder.Entity("DomainLayer.Loans", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ExcessDuty")
                        .HasColumnType("float");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LegalFees")
                        .HasColumnType("float");

                    b.Property<double>("LoanAmount")
                        .HasColumnType("float");

                    b.Property<double>("LoanPeriod")
                        .HasColumnType("float");

                    b.Property<double>("LoanType")
                        .HasColumnType("float");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("MonthlyPayment")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentFreq")
                        .HasColumnType("int");

                    b.Property<double>("ProcessingFee")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TakeHome")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("LoanCalculator");
                });

            modelBuilder.Entity("DomainLayer.Models.Banks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("FlatRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ReducingBalance")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Bank");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FlatRate = 20.0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Bank A",
                            ReducingBalance = 22.0
                        },
                        new
                        {
                            Id = 2,
                            FlatRate = 18.0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Bank B",
                            ReducingBalance = 25.0
                        });
                });

            modelBuilder.Entity("DomainLayer.LoanScheduleViewModel", b =>
                {
                    b.HasOne("DomainLayer.Loans", null)
                        .WithMany("LoanScheduleViewModel")
                        .HasForeignKey("LoansId");
                });

            modelBuilder.Entity("DomainLayer.Loans", b =>
                {
                    b.Navigation("LoanScheduleViewModel");
                });
#pragma warning restore 612, 618
        }
    }
}