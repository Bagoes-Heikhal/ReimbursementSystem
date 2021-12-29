using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    public class FinancesController : Controller
    {
        public IActionResult FDashboard()
        {
            return View();
        }
    }
}
