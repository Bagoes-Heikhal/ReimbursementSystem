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
    public class Employee_AttachmentsController : BaseController<Employee_Attachment, Employee_AttachmentRepository, string>
    {
        public Employee_AttachmentsController(Employee_AttachmentRepository repository) : base(repository)
        {
        }
    }
}
