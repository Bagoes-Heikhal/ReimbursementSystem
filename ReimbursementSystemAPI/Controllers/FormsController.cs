using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : BaseController<Form, FormRepository, int>
    {
        private FormRepository formRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
        public FormsController(FormRepository repository) : base(repository)
        {
        }

        //public int Form(FormVM formVM)
        //{
        //    Form form = new Form();
        //    {
        //        form.FormId = formVM.FormId;
        //        form.NIK = formVM.NIK;
        //        form.FirstName = formVM.FirstName;
        //        form.LastName = formVM.LastName;
        //        form.Gender = (formVM.Gender == "Male") ? Gender.Male : Gender.Female;
        //        form.BirthDate = formVM.BirthDate;
        //        form.Email = formVM.Email;
        //        form.Salary = formVM.Salary;
        //        form.Phone = formVM.Phone;
        //        form.DepartmentId = 1;
        //        form.JobId = 1;
        //        form.ReligionId = 1;
        //    }

        //    Account account = new Account();
        //    {
        //        account.EmployeeId = formVM.EmployeeId;
        //        account.Password = Hashing.HashPassword(formVM.Password);
        //        account.RoleId = 1;
        //    }

        //    context.Employees.Add(employee);
        //    context.Accounts.Add(account);
        //    context.SaveChanges();
        //    return 1;
        //}
    }
}
