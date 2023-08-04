using DomainLayer;
using DomainLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ILoanService
    {
        IEnumerable<LoanScheduleViewModel> GetAll();

        Task<LoanScheduleViewModel> Get (long id);

        Task Insert (LoanScheduleViewModel entity);

        Task Update (LoanScheduleViewModel entity);

        Task Delete (long id);
    }
}

