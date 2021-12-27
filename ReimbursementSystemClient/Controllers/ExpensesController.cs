using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<JsonResult> GetID()
        {
            var sessionEmail = HttpContext.Session.GetString("Email");
            var result = await expensesRepository.GetID(sessionEmail);
            HttpContext.Session.SetString("ExpenseID", result.ExpenseID.ToString());
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetExpense()
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = await expensesRepository.GetExpense(sessionId);
            return Json(result);
        }

        [HttpPost]
        public JsonResult NewExpense(ExpenseVM entity)
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = expensesRepository.NewExpense(entity, sessionId);
            return Json(result);
        }

        [HttpPut]
        public JsonResult Submit(ExpenseVM entity)
        {
            var sessionId = HttpContext.Session.GetString("EmployeeId");
            var result = expensesRepository.Submit(entity, sessionId);
            return Json(result);
        }

    }
}
