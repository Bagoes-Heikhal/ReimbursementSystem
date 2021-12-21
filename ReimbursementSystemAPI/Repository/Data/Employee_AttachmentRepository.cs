using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class Employee_AttachmentRepository : GeneralRepository<MyContext, Employee_Attachment, string>
    {
        public Employee_AttachmentRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
