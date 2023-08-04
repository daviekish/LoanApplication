using DomainLayer.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer
{
    public class LoanScheduleViewModel : BaseEntity
    {
        public Guid Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string LoanId { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public double LoanType { get; set; }

        public double LoanPeriod { get; set; }

        public DateTime DueDate { get; set; }

        public double LoanAmount { get; set; }

        public PaymentFreq PaymentFreq { get; set; }

        public double Interest { get; set; }

        public double PrincipalAmount { get; set; }

        public double Installment { get; set; }

        public double LoanBalance { get; set; }


    }
}
