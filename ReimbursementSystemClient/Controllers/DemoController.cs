using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemClient.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    [Route("demo")]
    public class DemoController : Controller
    {

        private UploadFileServiceImpl uploadFileServiceImpl;

        public DemoController()
        {
        }

        private DemoController( UploadFileServiceImpl _uploadFileServiceImpl)
        {
            uploadFileServiceImpl = _uploadFileServiceImpl;
        }

        [Route("~/")]
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("singleupload")]
        public async Task<IActionResult> SingleUpload(IFormFile file)
        {
            var bytes = new byte[file.OpenReadStream().Length + 1];
            file.OpenReadStream().Read(bytes, 0, bytes.Length);

            var result = await uploadFileServiceImpl.SingleUpload(file.FileName, bytes);
           
            return View("index");
        }

    }
}
