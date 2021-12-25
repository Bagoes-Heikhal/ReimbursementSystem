using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Controllers;
using ReimbursementSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Type = ReimbursementSystemAPI.Models.Type;

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
            //List<Category> CategoryList = Categories.ToList();
            //ViewBag.CountryList = new SelectList(CategoryList, "CategoryId", "CategoryName");
            return View();
        }
        //public JsonResult GetTypeList(int CategoryId)
        //{
        //    Configuration.ProxyCreationEnabled = false;
        //    List<Type> StateList = Types.Where(x => x.CountryId == CategoryId).ToList();
        //    return Json(StateList, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public JsonResult InsertForm(FormVM formVM)
        {
            var result = formRepository.InsertForm(formVM);
            return Json(result);
        }

    

       

    }
}
