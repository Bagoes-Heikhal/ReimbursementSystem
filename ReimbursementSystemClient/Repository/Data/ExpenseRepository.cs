using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Urls;
using ReimbursementSystemClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Repository.Data
{
    public class ExpenseRepository : GeneralRepository<Expense, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public ExpenseRepository(Address address, string request = "Expenses/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public HttpStatusCode NewExpense(ExpenseVM entity, string employeeId)
        {
            entity.EmployeeId = employeeId;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "ExpenseInsert", content).Result;
            return result.StatusCode;
        }

        public async Task<ExpenseIDVM> GetID(string email)
        {
            ExpenseIDVM entities = new ExpenseIDVM();

            using (var response = await httpClient.GetAsync(request + "GetID/" + email))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<ExpenseIDVM>(apiResponse);
            }
            return entities;
        }
    }
}
