using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.ViewModel
{
    public class JWTokenVM
    {
        public string Token { get; set; }
        public string Messages { get; set; }
        public string Email { get; set; }
        public string EmployeeId { get; set; }
    }
}