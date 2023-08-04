using DomainLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class AddLoanViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }
        
        public double LoanType { get; set; }

        public double LoanAmount { get; set; }

        public double LoanPeriod { get; set; }

        public DateTime StartDate { get; set; }

        public PaymentFreq PaymentFreq { get; set; }

        public double ProcessingFee { get; set; }

        public double ExcessDuty { get; set; }

        public double LegalFees { get; set; }

        public double TakeHome { get; set; }

       
        public List<Loans> Loan { get; set; }

        public List<LoanScheduleViewModel> LoanSchedule { get; set; }

    }
}
