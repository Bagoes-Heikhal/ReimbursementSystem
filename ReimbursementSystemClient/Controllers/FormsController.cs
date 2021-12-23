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



        [HttpPost]
        public JsonResult InsertForm(FormVM formVM)
        {
            var result = formRepository.InsertForm(formVM);
            return Json(result);
        }
    }
}
