using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class StudentsController : ControllerBase
    {
        [HttpPost("student")]
        public ActionResult Student([FromForm] AttachmentsVM std)
        {
            // Getting Name
            string name = std.Name;
            // Getting Image
            var image = std.Image;
            // Saving Image on Server
            if (image.Length > 0)
            {
                var filePath = Path.Combine("C:/Users/Gigabyte/source/repos/ReimbursementSystem/ReimbursementSystemAPI/Images/", image.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return Ok(new { status = true, message = "Student Posted Successfully" });
        }
    }
}
