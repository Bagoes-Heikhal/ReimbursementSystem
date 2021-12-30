using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Employee_AttachmentsController : BaseController<Employee_Attachment, Employee_AttachmentRepository, string>
    {
        private Employee_AttachmentRepository employee_attachmentRepository;
        public Employee_AttachmentsController(Employee_AttachmentRepository repository) : base(repository)
        {
            this.employee_attachmentRepository = repository;
        }

        [HttpPost("image")]
        public ActionResult File([FromForm] AttachmentsVM attachmentsVM)
        {
            var result = employee_attachmentRepository.File(attachmentsVM);
            switch (result)
            {
                case 1:
                    //return Ok();
                    return Ok(result);
                default:
                    return Ok(result);
            }
        }
    }
}
