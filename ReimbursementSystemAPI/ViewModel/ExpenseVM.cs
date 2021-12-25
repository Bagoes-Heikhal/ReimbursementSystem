using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.ViewModel
{
    public class ExpenseVM
    {
        public DateTime dateTime { get; set; }
        public string Status { get; set; }
        public string Approver { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public float Total { get; set; }

        public string EmployeeId { get; set; }
    }
}
