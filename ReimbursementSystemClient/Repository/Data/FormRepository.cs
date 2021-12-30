using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Urls;
using ReimbursementSystemClient.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Repository.Data
{
    public class FormRepository : GeneralRepository<Form, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public FormRepository(Address address, string request = "Forms/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<FormVM>> GetForm(int expenseid)
        {
            List<FormVM> entities = new List<FormVM>();

            using (var response = await httpClient.GetAsync(request + "FormData/" + expenseid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<FormVM>>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode InsertForm(FormVM entity, string expenseid)
        {
            entity.ExpenseId = Int32.Parse(expenseid);
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "multipart/form-data");
            var result = httpClient.PostAsync(address.link + request + "FormInsert", content).Result;
            return result.StatusCode;
        }


        //public HttpStatusCode InsertForm(FormVM entity, string expenseid)
        //{
        //    entity.ExpenseId = Int32.Parse(expenseid);

        //    using (var client = new HttpClient())
        //    {
        //        try
        //        {
        //            client.BaseAddress = new Uri(request);

        //            byte[] data;
        //            using (var br = new BinaryReader(entity.Attachments.OpenReadStream()))
        //                data = br.ReadBytes((int)entity.Attachments.OpenReadStream().Length);

        //            ByteArrayContent bytes = new ByteArrayContent(data);

        //            MultipartFormDataContent multiContent = new MultipartFormDataContent();

        //            multiContent.Add(bytes, "file", entity.Attachments.FileName);

        //            var result = client.PostAsync(address.link + request + "FormInsert", multiContent).Result;

        //            return result.StatusCode; 
        //        }
        //        catch (Exception)
        //        {
        //            return HttpStatusCode.InternalServerError; 
        //        }
        //    }
        //}

        public async Task<TotalVM> TotalExpenseForm(int expenseid)
        {
            TotalVM entities = null;

            using (var response = await httpClient.GetAsync(request + "TotalExpense/" + expenseid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<TotalVM>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode PutEditFrom(FormVM entity, int formid)
        {
            entity.FormId = formid;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "FormUpdate", content).Result;
            return result.StatusCode;
        }


        //public async Task Add(Dictionary<string, object> values, byte[] content, string fileName)
        //{
        //    using (var formDataContent = new MultipartFormDataContent())
        //    {
        //        formDataContent.Add(new ByteArrayContent(content), "files", fileName);
        //        formDataContent.Add(new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json"), "myJsonObject");
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            HttpResponseMessage response = await httpClient.PostAsync("", formDataContent);
        //            await EnsureResponse(response);
        //        }
        //    }
        //}

        //public HttpStatusCode Image(FormVM entity, string expenseid)
        //{
        //    using (var formDataContent = new MultipartFormDataContent())
        //    {
        //        formDataContent.Add(new ByteArrayContent(content), "files", fileName);
        //    }
        //    entity.ExpenseId = Int32.Parse(expenseid);
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        //    var result = httpClient.PostAsync(address.link + request + "ImageInsert", content).Result;
        //    return result.StatusCode;
        //}
    }
}
