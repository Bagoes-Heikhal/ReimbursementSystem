using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Repository.Interface
{
    interface UploadFileService
    {
        public Task<HttpResponseMessage> SingleUpload(string fileName, byte[] bytes);
    }
}
