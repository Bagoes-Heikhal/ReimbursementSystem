using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class ExpenseRepository : GeneralRepository<MyContext, Expense, int>
    {
        public ExpenseRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
