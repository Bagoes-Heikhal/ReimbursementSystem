using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class FormRepository : GeneralRepository<MyContext, Form, int>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;
        public FormRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
        }

        public int Form(FormVM fromVM)
        {
            Form form = new Form();
            {
                form.Receipt_Date = fromVM.Receipt_Date;
                form.Start_Date = fromVM.Start_Date;
                form.End_Date = fromVM.End_Date;

                switch (fromVM.Category)
                {
                    case "Transportation":
                        form.Category = ViewModel.Category.Transportation;
                        break;
                    case "Parking":
                        form.Category = ViewModel.Category.Parking;
                        break;
                    case "Medical":
                        form.Category = ViewModel.Category.Medical;
                        break;
                    case "Lodging":
                        form.Category = ViewModel.Category.Lodging;
                        break;
                    default:
                        break;
                }
                form.Payee = fromVM.Payee;
                form.Description = fromVM.Description;
                form.Total = fromVM.Total;
                form.Attachments = fromVM.Attachments;
            }

            return 1;
        }
    }
}
