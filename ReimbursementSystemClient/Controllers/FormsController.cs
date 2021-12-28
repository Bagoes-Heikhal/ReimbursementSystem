using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class FormsController : BaseController<Form, FormRepository, string>
    {

        private readonly FormRepository formRepository;
        public IConfiguration _iconfiguration;
        public FormsController(FormRepository repository,  IConfiguration configuration) : base(repository)
        {
            this.formRepository = repository;
            this._iconfiguration = configuration;
        }

        //public IActionResult Index()
        //{

        //    List<Category> CategoryList = context.Categories1.ToList();
        //    ViewBag.CategoryList = new SelectList(CategoryList, "CategoryId", "CategoryName");
        //    return View();
        //}
        //public JsonResult GetTypeList(int CategoryId)
        //{
        //    //context._iconfiguration.ProxyCreationEnabled = false;
        //    List<Type> TypeList = context.Types.Where(x => x.CategoryId == CategoryId).ToList();
        //    return Json(TypeList);

        //}

        [Route("~/forms/getform/{expenseid}")]
        public async Task<JsonResult> GetForm(int expenseid)
        {

            var result = await formRepository.GetForm(expenseid);
            return Json(result);
        }

        [HttpPost]
        public JsonResult InsertForm(FormVM formVM)
        {
            var sessionExpense = HttpContext.Session.GetString("ExpenseID");
            var result = formRepository.InsertForm(formVM, sessionExpense);
            return Json(result);
        }

        [Route("~/forms/TotalExpenseForm/{expenseid}")]
        public async Task<JsonResult> TotalExpenseForm(int expenseid)
        {
            var result = await formRepository.TotalExpenseForm(expenseid);
            return Json(result);
        }
    }
}
