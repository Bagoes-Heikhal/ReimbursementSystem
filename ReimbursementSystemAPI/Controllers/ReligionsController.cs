using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReligionsController : BaseController<Religion, ReligionRepository, int>
    {
        public ReligionsController(ReligionRepository repository) : base(repository)
        {
        }
    }
}
