using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReimbursementSystemClient.Controllers
{
    public class ManagerController : Controller
    {
        // GET: /<controller>/
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Dashboard2()
        {
            return View();
        }

        public IActionResult Dashboard3()
        {
            return View();
        }

        [Authorize]
        public IActionResult Manager()
        {
            return View();
        }
    }
}
