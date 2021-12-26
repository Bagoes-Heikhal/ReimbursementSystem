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

        public int NewForm(FormVM fromVM)
        {
            Form form = new Form();
            {
                form.Receipt_Date = fromVM.Receipt_Date;
                form.Start_Date = fromVM.Start_Date;
                form.End_Date = fromVM.End_Date;

                switch (fromVM.Category)
                {
                    case 1:
                        form.Category = Category.Transportation;
                        break;
                    case 2:
                        form.Category = Category.Parking;
                        break;
                    case 3:
                        form.Category = Category.Medical;
                        break;
                    case 4:
                        form.Category = Category.Lodging;
                        break;
                    //case "Transportation":
                    //    form.Category = ViewModel.Category.Transportation;
                    //    break;
                    //case "Parking":
                    //    form.Category = ViewModel.Category.Parking;
                    //    break;
                    //case "Medical":
                    //    form.Category = ViewModel.Category.Medical;
                    //    break;
                    //case "Lodging":
                    //    form.Category = ViewModel.Category.Lodging;
                    //    break;
                    default:
                        break;
                }
                form.Payee = fromVM.Payee;
                form.Description = fromVM.Description;
                form.Total = fromVM.Total;
                form.Attachments = fromVM.Attachments;
                form.ExpenseId = fromVM.ExpenseId;
            }
            return 1;
        }

        public IEnumerable<FormVM> GetForm(int expenseid)
        {
            var register = from a in context.Expenses where a.ExpenseId == expenseid
                           join b in context.Forms on a.ExpenseId equals b.ExpenseId
                           select new FormVM()
                           {
                               Receipt_Date = b.Receipt_Date,
                               Total = b.Total,
                               Payee = b.Payee,
                               Type = b.Type,
                               Category = 3,
                               Description = b.Description,
                           };

            return register.ToList();
        }
    }
}
