using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Repository.Interface
{
    public class UploadFileServiceImpl
    {
        private string Base_URL = "https://localhost:44350/API/demo/";

        public Task<HttpResponseMessage> SingleUpload(string fileName, byte[] bytes)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                var multipartFormDataContent = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(bytes);
                multipartFormDataContent.Add(fileContent, "file", fileName);
                return client.PostAsync("singleupload", multipartFormDataContent);
            }
            catch (Exception)
            {

                return null;
            }
        }


    }
}
