﻿using Microsoft.AspNetCore.Http;
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

        [HttpPut("ExpenseUpdate")]
        public ActionResult ExpenseFormUpdate(ExpenseVM expenseVM)
        {
            var result = expenseRepository.ExpenseFormUpdate(expenseVM);
            switch (result)
            {
                case 1:
                    return Ok();
                default:
                    return BadRequest();
            }
        }

        [HttpGet("ExpenseData/{employeeid}")]
        public ActionResult GetExpense(string employeeid)
        {
            var result = expenseRepository.GetExpense(employeeid);
            
            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("GetID/{email}")]
        public ActionResult ExpesnseID(string email)
        {
            var result = expenseRepository.ExpesnseID(email);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }



    }
}
