﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Controllers;
using ReimbursementSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    public class ExpensesController : BaseController<Expense, ExpenseRepository, string>
    {
        private readonly ExpenseRepository expensesRepository;

        public ExpensesController(ExpenseRepository repository) : base(repository)
        {
            this.expensesRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Expense()
        {
            return View();
        }

        [Authorize]
        public IActionResult Reimbusment()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<JsonResult> GetID()
        //{
        //    var sessionEmail = HttpContext.Session.GetString("Email");
        //    var result = await expensesRepository.GetID(sessionEmail);
        //    HttpContext.Session.SetString("ExpenseID", result.ExpenseID.ToString());
        //    return Json(result);
        //}

        [HttpGet]
        public async Task<JsonResult> GetExpense()
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = await expensesRepository.GetExpense(sessionId);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetExpenseModified()
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = await expensesRepository.GetExpenseModified(sessionId);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> NewExpense(ExpenseVM entity)
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = expensesRepository.NewExpense(entity, sessionId);

            var sessionEmail = HttpContext.Session.GetString("Email");
            var result2 = await expensesRepository.GetID(sessionEmail);
            HttpContext.Session.SetString("ExpenseID", result2.ExpenseID.ToString());

            return Json(result);
        }

        [HttpPut]
        public JsonResult Submit(ExpenseVM entity)
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var sessionEmail = HttpContext.Session.GetString("Email");
            var result = expensesRepository.Submit(entity, sessionId, sessionEmail);
            return Json(result);
        }

        [Route("~/Expenses/EditExpense/{expenseid}")]
        public JsonResult EditExpense(int expenseid)
        {
            HttpContext.Session.SetString("ExpenseID", expenseid.ToString());
            return Json(expenseid);
        }

        [Route("~/Expenses/ExpenseCall/")]
        public JsonResult EditExpenseCall()
        {
            var expenseSession = HttpContext.Session.GetString("ExpenseID");
            HttpContext.Session.SetString("ExpenseID", expenseSession);
            return Json(expenseSession);
        }


        //<!----------------- Finances ------------------->

        [HttpGet]
        public async Task<JsonResult> GetExpenseFinance()
        {
            var result = await expensesRepository.GetExpenseFinance();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetExpenseFinanceReject()
        {
            var result = await expensesRepository.GetExpenseFinanceReject();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetExpenseFinanceAll()
        {
            var result = await expensesRepository.GetExpenseFinanceAll();
            return Json(result);
        }

        [HttpPut]
        public JsonResult NonSessionSubmit(ExpenseVM entity)
        {
            var result = expensesRepository.NonSessionSubmit(entity);
            return Json(result);
        }


    }
}
