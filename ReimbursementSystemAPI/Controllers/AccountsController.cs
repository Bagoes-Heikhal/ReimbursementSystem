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
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository accountRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public AccountsController(AccountRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.accountRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }

        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
            switch (result)
            {
                case 1:
                    return Ok(new { Messages = "Login Sucsses" });
                default:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Login Fail" });
            }

            
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var result = accountRepository.Register(registerVM);
            switch (result)
            {
                case 1:
                    return Ok(new { Status = HttpStatusCode.BadRequest, Messages = "Register Sucsses" });
                default:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Register Fail" });
            }
            
        }
    }
}
