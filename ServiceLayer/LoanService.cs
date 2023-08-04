using DomainLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class LoanService : ILoanService
    {
        private readonly IRepository<LoanScheduleViewModel> iRepository;

        public LoanService(IRepository<LoanScheduleViewModel> iRepository)
        {
            this.iRepository = iRepository;
        }

        public async Task Delete(long id)
        {
            LoanScheduleViewModel entity = await iRepository.Get(id);
            iRepository.Remove(entity);
            await iRepository.SaveChanges();
        }

        public async Task<LoanScheduleViewModel> Get(long id)
        {
            return await iRepository.Get(id);
        }

        public IEnumerable<LoanScheduleViewModel> GetAll()
        {
            return iRepository.GetAll();
        }
        public async Task Insert(LoanScheduleViewModel entity)
        {
            await iRepository.Insert(entity);
        }

        public async Task Update(LoanScheduleViewModel entity)
        {
            await iRepository.Update(entity);
        }
    }
}
