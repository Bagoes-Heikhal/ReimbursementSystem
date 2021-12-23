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
using System.Net;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : BaseController<Form, FormRepository, int>
    {
        private FormRepository formRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
        public FormsController(FormRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.formRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }

        [HttpPost("FormInsert")]
        public ActionResult Register(FormVM fromVM)
        {
            var result = formRepository.Form(fromVM);
            switch (result)
            {
                case 1:
                    return Ok();
                default:
                    return BadRequest();
            }
        }
    }
}
