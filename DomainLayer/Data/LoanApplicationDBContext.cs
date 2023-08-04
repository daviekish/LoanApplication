using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Data
{
    public class LoanApplicationDBContext : DbContext
    {
        public LoanApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Banks>().HasData(
                new Banks() { Id=1, Name = "Bank A", FlatRate = 20, ReducingBalance = 22 },
                new Banks() { Id=2, Name = "Bank B", FlatRate = 18, ReducingBalance = 25 }
                );
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Loans> LoanCalculator { get; set; }
        public DbSet <Banks> Bank { get; set; }

        public DbSet<LoanScheduleViewModel> LoanScheduleViewModel { get; set;}


    }
}

