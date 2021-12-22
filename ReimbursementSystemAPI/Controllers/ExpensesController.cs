using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : BaseController<Expense, ExpenseRepository, int>
    {
        private ExpenseRepository expenseRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public ExpensesController(ExpenseRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.expenseRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }
    }
}
