using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class FormRepository : GeneralRepository<MyContext, Form, int>
    {
        public FormRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
