using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository accountRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public AccountsController(AccountRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.accountRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }

        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
            switch (result)
            {
                case 1:
                    //var getUserData = (from a in context.Employees
                    //                   where a.Email == loginVM.Email 
                    //                   join b in context.Accounts on a.EmployeeId equals b.EmployeeId
                    //                   join c in context.Roles on b.RoleId equals c.RoleId
                    //                   select new
                    //                   {
                    //                       Employee = a.Email,
                    //                       Role = c.Name
                    //                   }).ToList();

                    //var claims = new List<Claim>
                    //{
                    //    new Claim(JwtRegisteredClaimNames.Email, getUserData[0].Employee),
                    //    new Claim(ClaimTypes.Role, getUserData[0].Role)
                    //};

                    ////foreach (var userRole in getUserData)
                    ////{
                    ////    claims.Add(new Claim(ClaimTypes.Role, userRole.Role));
                    ////}

                    //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    //var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    //var token = new JwtSecurityToken(
                    //    _configuration["Jwt:Issuer"],
                    //    _configuration["Jwt:Audience"],
                    //    claims,
                    //    expires: DateTime.UtcNow.AddMinutes(10),
                    //    signingCredentials: signIn
                    //    );

                    //var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    //claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                    return Ok(new JWTokenVM { Token = "Login Sucsses", Messages = "Login Sucsses" });
                    //return Ok(new JWTokenVM { Token = idtoken, Messages = "Login Sucsses" });
                case 2:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = $"Password salah" });
                case 3:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = $"Akun tidak ditemukan" });
                default:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = $"Gagal Login" });
            }
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var result = accountRepository.Register(registerVM);
            switch (result)
            {
                case 1:
                    return Ok(new { Status = HttpStatusCode.BadRequest, Messages = "Register Sucsses" });
                default:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Register Fail" });
            }
            
        }

        [HttpPost("Insert")]
        public ActionResult Insert(RegisterVM registerVM)
        {
            var result = accountRepository.Register(registerVM);
            switch (result)
            {
                case 1:
                    return Ok(new { Status = HttpStatusCode.BadRequest, Messages = "Register Sucsses" });
                default:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Register Fail" });
            }

        }
    }
}
