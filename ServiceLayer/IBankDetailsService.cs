using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IBankDetailsService
    {

        IEnumerable<Banks> GetAll();

        Task<Banks> Get(long id);

        Task Insert(Banks entity);

        Task Update(Banks entity);

        Task Delete(long id);


    }
}
