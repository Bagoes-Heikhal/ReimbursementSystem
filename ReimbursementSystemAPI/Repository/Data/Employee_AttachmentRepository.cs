using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class Employee_AttachmentRepository : GeneralRepository<MyContext, Employee_Attachment, string>
    {
        private readonly MyContext context;
        public Employee_AttachmentRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int File(AttachmentsVM attachmentsVM)
        {

            var image = attachmentsVM.Image;
            // Saving Image on Server

            Employee_Attachment atc = new Employee_Attachment();
            var filePath = Path.Combine("C:/Users/Gigabyte/source/repos/ReimbursementSystem/ReimbursementSystemAPI/Images/", image.FileName);

            if (image.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            atc.FilePath = filePath;
            context.Employee_Attachments.Add(atc);
            var a = context.SaveChanges();
            return a;
        }
    }
}
