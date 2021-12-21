using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class ReimbusmentRepository : GeneralRepository<MyContext, Reimbusment, string>
    {
        public ReimbusmentRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
