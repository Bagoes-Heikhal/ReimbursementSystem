using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Controllers;
using ReimbursementSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Category = ReimbursementSystemAPI.Models.Category;
using Type = ReimbursementSystemAPI.Models.Type;

namespace ReimbursementSystemClient.Controllers
{
   
    public class FormsController : BaseController<Form, FormRepository, string>
    {

        private readonly FormRepository formRepository;
        private readonly MyContext context;
        public IConfiguration _iconfiguration;
        public FormsController(FormRepository repository, MyContext context, IConfiguration configuration) : base(repository)
        {
            this.context = context;
            this.formRepository = repository;
            this._iconfiguration = configuration;
        }

        
        public IActionResult Index()
        {
           
            List<Category> CategoryList = context.Categories1.ToList();
            ViewBag.CategoryList = new SelectList(CategoryList, "CategoryId", "CategoryName");
            return View();
        }
        public JsonResult GetTypeList(int CategoryId)
        {
            //context._iconfiguration.ProxyCreationEnabled = false;
            List<Type> TypeList = context.Types.Where(x => x.CategoryId == CategoryId).ToList();
            return Json(TypeList);

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
