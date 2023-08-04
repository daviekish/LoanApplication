using DomainLayer;
using DomainLayer.Data;
using DomainLayer.Enum;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;

namespace Loan_Aplication.Controllers
{
    public class LoanCalculatorController : Controller
    {
        private readonly LoanApplicationDBContext loanApplication;
        private readonly LoanScheduleViewModel loanScheduleViewModel;
        private readonly Banks bank;

        public LoanCalculatorController(LoanApplicationDBContext LoanApplication, LoanScheduleViewModel LoanScheduleViewModel, Banks bank)
        {
            loanApplication = LoanApplication;
            loanScheduleViewModel = LoanScheduleViewModel;
            this.bank = bank;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var loan = await loanApplication.LoanCalculator.ToListAsync();
            return View(loan);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddLoanViewModel();
            return View(model);
        }

        [HttpPost]  
        public async Task<IActionResult> Add(AddLoanViewModel model, int id, string name)
        {
           

            double ProcessingFee = model.LoanAmount * 0.03; // 3% processing Fee
            double ExcessDuty = ProcessingFee * 0.2; //20% excess duty
            double LegalFees = 10000; // 10000 ksh legal fees

            double monthlyPayment = model.LoanAmount / model.LoanPeriod;

            model.ProcessingFee = ProcessingFee;
            model.LegalFees = LegalFees;
            model.ExcessDuty = ExcessDuty;
            model.TakeHome = model.LoanAmount - ProcessingFee - ExcessDuty - LegalFees;



            var loan = new Loans()
            {
                Id = Guid.NewGuid(),
                FullName = model.FullName,
                Name = model.Name,
                LoanType = model.LoanType,
                LoanAmount = model.LoanAmount,
                LoanPeriod = model.LoanPeriod,
                StartDate = model.StartDate,
                PaymentFreq = model.PaymentFreq,
                ProcessingFee = model.ProcessingFee,
                ExcessDuty = model.ExcessDuty,
                LegalFees = model.LegalFees,
                MonthlyPayment = monthlyPayment,
                TakeHome = model.TakeHome,
                ModifiedDate = DateTime.Now,

            };

            await loanApplication.LoanCalculator.AddAsync(loan);
            
            await loanApplication.SaveChangesAsync();



            //Loan Schedule

            double l = model.LoanPeriod;

            //IMPORTED CODE
            double totalPrincipal = 0;
            for (int i = 0; i < l; i++)
            {
                LoanScheduleViewModel loanSchedule = new LoanScheduleViewModel();

                loanSchedule = new LoanScheduleViewModel();

                loanSchedule.Id = model.Id;
                bank.Id = id;
                bank.Name = name;

               
                
                loanSchedule.LoanAmount = model.LoanAmount;

                var bnk = await loanApplication.Bank.FirstOrDefaultAsync( x=> x.Name == name);

                if (bnk == null)
                {
                    return NotFound("Value not found ");
                }


                loanSchedule.DueDate = i == 0 ? (model.PaymentFreq == PaymentFreq.Monthly ? Convert.ToDateTime(model.StartDate).AddMonths(1) :
                    model.PaymentFreq == PaymentFreq.Quarterly ? Convert.ToDateTime(model.StartDate).AddMonths(3) :
                    model.PaymentFreq == PaymentFreq.Every6Months ? Convert.ToDateTime(model.StartDate).AddMonths(6) :
                    Convert.ToDateTime(model.StartDate).AddMonths(12)) :
                    (model.PaymentFreq == PaymentFreq.Monthly ? Convert.ToDateTime(model.StartDate).AddMonths(1 + i) :
                    model.PaymentFreq == PaymentFreq.Quarterly ? Convert.ToDateTime(model.StartDate).AddMonths(3 + 3 * i) :
                    model.PaymentFreq == PaymentFreq.Every6Months ? Convert.ToDateTime(model.StartDate).AddMonths(6 + 6 * i) :
                    Convert.ToDateTime(model.StartDate).AddMonths(12 + 12 * i));
               
                loanSchedule.PrincipalAmount = model.LoanAmount / model.LoanPeriod;
                

                loanSchedule.Interest = i == 0 ? (model.PaymentFreq == PaymentFreq.Monthly ? model.LoanType == bnk.FlatRate ? model.LoanAmount * (100 * 12) :
                    model.LoanAmount *  bnk.ReducingBalance / (12 * 100) : model.PaymentFreq == PaymentFreq.Quarterly ? model.LoanType == bnk.FlatRate ? model.LoanAmount * bnk.FlatRate * 3 / (100 * 12) :
                            model.LoanAmount * bnk.ReducingBalance *  3 / (12 * 100) : model.PaymentFreq == PaymentFreq.Every6Months ? model.LoanType == bnk.FlatRate ? model.LoanAmount * bnk.FlatRate * 6 / (100 * 12) :
                            model.LoanAmount * bnk.ReducingBalance *  6 / (12 * 100) : model.LoanType == bnk.FlatRate ? model.LoanAmount * bnk.FlatRate *  12 / (100 * 12) :
                            model.LoanAmount * bnk.ReducingBalance * 12 / (12 * 100)) :
                 
                            (model.PaymentFreq == PaymentFreq.Monthly ? model.LoanType == bnk.FlatRate ? model.LoanAmount *  bnk.FlatRate / (100 * 12) :
                            (model.LoanAmount - totalPrincipal) * bnk.ReducingBalance / (12 * 100) : model.PaymentFreq == PaymentFreq.Quarterly ? model.LoanType == bnk.FlatRate ? model.LoanAmount * bnk.FlatRate * 3 / (100 * 12) :
                            (model.LoanAmount - totalPrincipal) * bnk.ReducingBalance * 3 / (12 * 100) : model.PaymentFreq == PaymentFreq.Every6Months ? model.LoanType == bnk.FlatRate ? model.LoanAmount * bnk.FlatRate * 6 / (100 * 12) :
                            (model.LoanAmount - totalPrincipal) * bnk.ReducingBalance * 6 / (12 * 100) : model.LoanType == bnk.FlatRate ? model.LoanAmount * bnk.FlatRate * 12 / (100 * 12) :
                            (model.LoanAmount - totalPrincipal) * bnk.ReducingBalance *  12 / (12 * 100));
            loanSchedule.Installment = loanSchedule.PrincipalAmount + loanSchedule.Interest;
            totalPrincipal += loanSchedule.PrincipalAmount;
            loanSchedule.LoanBalance = model.LoanAmount - totalPrincipal;


                var newSchedule = new LoanScheduleViewModel()
                {
                    Id = Guid.NewGuid(),
                    
                    Name = loanSchedule.Name = model.Name,
                    FullName = loanSchedule.FullName = model.FullName,
                    LoanType = loanSchedule.LoanType = model.LoanType,
                    LoanPeriod = loanSchedule.LoanPeriod = model.LoanPeriod,
                    DueDate = loanSchedule.DueDate,
                    LoanAmount = loanSchedule.LoanAmount = model.LoanAmount,
                    PaymentFreq = loanSchedule.PaymentFreq = model.PaymentFreq,
                    Interest = loanSchedule.Interest,
                    PrincipalAmount = loanSchedule.PrincipalAmount,
                    Installment = loanSchedule.Installment,
                    LoanBalance = loanSchedule.LoanBalance,
                    
                };


                await loanApplication.AddAsync (newSchedule);
                await loanApplication.LoanScheduleViewModel.AddAsync(newSchedule);
                await loanApplication.SaveChangesAsync();
            }
            return View (model);

        }
    

        [HttpGet]

        public async Task<IActionResult> Create (string name)
        {



            var loanScheduleData = await loanApplication.LoanScheduleViewModel.Where(x => x.FullName == name).OrderBy(x => x.DueDate).ToListAsync();


            if (loanScheduleData != null) 
            {
                return View (loanScheduleData);
            }

            return View();

          
        }

    }
}










//List<LoanScheduleViewModel> loanSchedules();











//loanSchedule = new LoanScheduleViewModel
//{
//    FullName = loanSchedule.FullName,
//    Name = loanSchedule.Name,
//    LoanAmount = loanSchedule.LoanAmount,
//    LoanType = loanSchedule.LoanType,
//    LoanPeriod= loanSchedule.LoanPeriod,
//    DueDate = loanSchedule.DueDate,
//    PaymentFreq=loanSchedule.PaymentFreq,
//    PrincipalAmount=loanSchedule.PrincipalAmount,
//    Installment=loanSchedule.Installment,
//    Interest=loanSchedule.Interest,
//    LoanBalance=loanSchedule.LoanBalance
//};





//if (model.BankName == BankServices.BankA)
//{
//    rateFee = model.LoanType == Service.FlatRate ? model.LoanAmount * 0.01666667 : model.LoanAmount * 0.01833333;

//}
//else if (model.BankName == BankServices.BankB)
//{
//    rateFee = model.LoanType == Service.FlatRate ? model.LoanAmount * 0.015 : model.LoanAmount * 0.02083333;

//}
//else
//{
//    return BadRequest();
//}