using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class ReligionRepository : GeneralRepository<MyContext, Religion, int>
    {
        public ReligionRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
