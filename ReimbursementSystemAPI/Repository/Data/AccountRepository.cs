using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;

        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
        }
    }
}
