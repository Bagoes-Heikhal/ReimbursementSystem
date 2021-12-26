using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public FormsController(FormRepository repository) : base(repository)
        {
            this.formRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("{expenseid}")]
        public async Task<JsonResult> GetForm(int expenseid)
        {
            var result = await formRepository.GetForm(expenseid);
            return Json(result);
        }

        [HttpPost]
        public JsonResult InsertForm(FormVM formVM)
        {
            var result = formRepository.InsertForm(formVM);
            return Json(result);
        }
    }
}
