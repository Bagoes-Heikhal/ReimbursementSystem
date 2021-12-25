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
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await accountRepository.Auth(login);
            var token = jwtToken.Token;
            var email = jwtToken.Email;
            var id = jwtToken.EmployeeId;

            if (token == null)
            {
                return Json(Url.Action("login", "Home"));
            }

            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("EmployeeId", id);

            return Json(Url.Action("Reimbusment", "Reimbusments"));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login", "Home");
        }


    }
}
