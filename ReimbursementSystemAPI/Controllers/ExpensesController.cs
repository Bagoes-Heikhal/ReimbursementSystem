using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using ReimbursementSystemAPI.ViewModel;
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

        [HttpPost("ExpenseInsert")]
        public ActionResult ExpenseForm(ExpenseVM expenseVM)
        {
            var result = expenseRepository.ExpenseForm(expenseVM);
            switch (result)
            {
                case 1:
                    return Ok();
                default:
                    return BadRequest();
            }
        }


        [HttpPost("ExpenseInsert2")]
        public ActionResult ExpenseForm2(LoginVM loginVM)
        {
            var result = expenseRepository.ExpesnseID(loginVM);

            if (result != null)
            {
                return Ok( new { result = result });
            }
            return NotFound(new { result = result });
        }

    }
}
