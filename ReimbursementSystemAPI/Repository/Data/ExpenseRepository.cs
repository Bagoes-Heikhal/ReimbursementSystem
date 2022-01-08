using API.Hash;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class ExpenseRepository : GeneralRepository<MyContext, Expense, int>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;
        public ExpenseRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
        }

        public int ExpenseForm(ExpenseVM expenseVM)
        {
            Expense expense = new Expense();
            {
                expense.Approver = expenseVM.Approver;
                expense.Description = expenseVM.Description;
                expense.Total = expenseVM.Total;
                expense.Submitted = (expenseVM.Submitted == null) ? DateTime.Now : expenseVM.Submitted;
                switch (expenseVM.Status)
                {
                    case 0:
                        expense.Status = Status.Approved;
                        break;
                    case 1:
                        expense.Status = Status.Rejected;
                        break;
                    case 2:
                        expense.Status = Status.Canceled;
                        break;
                    case 3:
                        expense.Status = Status.Posted;
                        break;
                    case 4:
                        expense.Status = Status.Draft;
                        break;
                    case 5:
                        expense.Status = Status.ApprovedByManager;
                        break;
                    case 6:
                        expense.Status = Status.ApprovedByFinance;
                        break;
                    case 7:
                        expense.Status = Status.RejectedByManager;
                        break;
                    case 8:
                        expense.Status = Status.RejectedByFinance;
                        break;
                    case 9:
                        expense.Status = Status.OnManager2;
                        break;
                    case 10:
                        expense.Status = Status.ApprovedByManager2;
                        break;
                    case 11:
                        expense.Status = Status.OnManager3;
                        break;
                    case 12:
                        expense.Status = Status.ApprovedByManager3;
                        break;
                    default:
                        break;
                }
                expense.EmployeeId = expenseVM.EmployeeId;
            }

            context.Expenses.Add(expense);    
            context.SaveChanges();
            return 1;
        }

        public int ExpenseFormUpdate(ExpenseVM expenseVM)
        {
            var data = (from a in context.Employees 
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where b.ExpenseId == expenseVM.ExpenseId
                            select new { expenses = b }).Single();

            var expense = data.expenses;
            expense.Approver = expenseVM.Approver;
            expense.CommentManager = expenseVM.CommentManager;
            expense.CommentFinace = expenseVM.CommentFinace;
            expense.Submitted = (expenseVM.Submitted == null) ? DateTime.Now : expenseVM.Submitted;
            expense.Purpose = expenseVM.Purpose;
            expense.Description = expenseVM.Description;
            expense.Total = expenseVM.Total;
            switch (expenseVM.Status)
            {
                case 0:
                    expense.Status = Status.Approved;
                    break;
                case 1:
                    expense.Status = Status.Rejected;
                    break;
                case 2:
                    expense.Status = Status.Canceled;
                    break;
                case 3:
                    expense.Status = Status.Posted;
                    break;
                case 4:
                    expense.Status = Status.Draft;
                    break;
                case 5:
                    expense.Status = Status.ApprovedByManager;
                    break;
                case 6:
                    expense.Status = Status.ApprovedByFinance;
                    break;
                case 7:
                    expense.Status = Status.RejectedByManager;
                    break;
                case 8:
                    expense.Status = Status.RejectedByFinance;
                    break;
                case 9:
                    expense.Status = Status.OnManager2;
                    break;
                case 10:
                    expense.Status = Status.ApprovedByManager2;
                    break;
                case 11:
                    expense.Status = Status.OnManager3;
                    break;
                case 12:
                    expense.Status = Status.ApprovedByManager3;
                    break;
                default:
                    break;
            }
            expense.EmployeeId = expenseVM.EmployeeId;
            var expensess = expense;
            context.SaveChanges();
            return 1;
        }

        public int LastExpense()
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        select new { id = b.ExpenseId }).LastOrDefault();

            return 1;
        }

        public ExpenseIDVM ExpesnseID(string email)
        {
            var data = (from a in context.Employees
                        where a.Email == email
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        select new ExpenseIDVM()
                        { ExpenseID = b.ExpenseId }).ToList().LastOrDefault();

            return data;
        }

        public IEnumerable<ExpenseVM> GetExpense(string employeeid)
        {
            var register = from a in context.Employees where a.EmployeeId == employeeid
                           join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                           join c in context.Departements on a.DepartmentId equals c.DepartmentId
                           select new ExpenseVM()
                           {
                               Approver = (from a in context.Employees where a.EmployeeId == c.ManagerId select a.FirstName + " " + a.LastName).Single().ToString(),
                               Submitted = b.Submitted,
                               ExpenseId = b.ExpenseId,
                               Purpose = b.Purpose,
                               CommentFinace = b.CommentFinace,
                               CommentManager = b.CommentManager,
                               Status = (int)b.Status,
                               Total = b.Total,
                               Description = b.Description,
                           };
            var data = register.ToList();
            return register.ToList();
        }

        //<!----------------- Finances -------------------> 
        public IEnumerable<ExpenseManager> GetExpenseFinance()
        {
            var expense = from a in context.Employees
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where b.Status == Status.ApprovedByManager || b.Status == Status.ApprovedByManager2 || b.Status == Status.ApprovedByManager3
                          select new ExpenseManager()
                          {
                              Status = (int)b.Status,
                              EmployeeId = b.EmployeeId,
                              ExpenseId = b.ExpenseId,
                              Name = a.FirstName + " " + a.LastName,
                              DateTime = b.Submitted,
                              Total = b.Total,
                              Description = b.Description,
                              Purpose = b.Purpose
                          };
            return expense.ToList();
        }
        public IEnumerable<ExpenseManager> GetExpenseFinanceReject()
        {
            var expense = from a in context.Employees
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where b.Status == Status.RejectedByFinance
                          select new ExpenseManager()
                          {
                              Status = (int)b.Status,
                              EmployeeId = b.EmployeeId,
                              ExpenseId = b.ExpenseId,
                              Name = a.FirstName + " " + a.LastName,
                              DateTime = b.Submitted,
                              Total = b.Total,
                              Description = b.Description,
                              Purpose = b.Purpose
                          };
            return expense.ToList();
        }


        //<!----------------- Manager -------------------> 

        public IEnumerable<ExpenseManager> GetExpenseManager()
        {
            var expense = from a in context.Employees
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where b.Status == Status.Posted
                          select new ExpenseManager()
                          {
                              Status = (int)b.Status,
                              EmployeeId = b.EmployeeId,
                              ExpenseId = b.ExpenseId,
                              Name = a.FirstName + " " + a.LastName,
                              DateTime = b.Submitted,
                              Total = b.Total,
                              Description = b.Description,
                              Purpose = b.Purpose
                          };
            return expense.ToList();
        }

        public IEnumerable<ExpenseManager> GetExpenseSManager()
        {
            var expense = from a in context.Employees
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where b.Status == Status.OnManager2
                          select new ExpenseManager()
                          {
                              Status = (int)b.Status,
                              EmployeeId = b.EmployeeId,
                              ExpenseId = b.ExpenseId,
                              Name = a.FirstName + " " + a.LastName,
                              DateTime = b.Submitted,
                              Total = b.Total,
                              Description = b.Description,
                              Purpose = b.Purpose
                          };
            return expense.ToList();
        }

        public IEnumerable<ExpenseManager> GetExpenseDirector()
        {
            var expense = from a in context.Employees
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where b.Status == Status.OnManager3
                          select new ExpenseManager()
                          {
                              Status = (int)b.Status,
                              EmployeeId = b.EmployeeId,
                              ExpenseId = b.ExpenseId,
                              Name = a.FirstName + " " + a.LastName,
                              DateTime = b.Submitted,
                              Total = b.Total,
                              Description = b.Description,
                              Purpose = b.Purpose
                          };
            return expense.ToList();
        }

        public IEnumerable<ExpenseManager> GetExpenseManagerReject()
        {
            var expense = from a in context.Employees
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where b.Status == Status.RejectedByManager
                          select new ExpenseManager()
                          {
                              Status = (int)b.Status,
                              EmployeeId = b.EmployeeId,
                              ExpenseId = b.ExpenseId,
                              Name = a.FirstName + " " + a.LastName,
                              DateTime = b.Submitted,
                              Total = b.Total,
                              Description = b.Description,
                              Purpose = b.Purpose
                          };
            return expense.ToList();
        }


        //<!----------------- Manager & Finances -------------------> 

        public IEnumerable<ExpenseManager> GetExpensePosted()
        {
            var expense = from a in context.Employees
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where b.Status != Status.Draft
                          select new ExpenseManager()
                          {
                              Status = (int)b.Status,
                              EmployeeId = b.EmployeeId,
                              ExpenseId = b.ExpenseId,
                              Name = a.FirstName + " " + a.LastName,
                              DateTime = b.Submitted,
                              Total = b.Total,
                              Description = b.Description,
                              Purpose = b.Purpose
                          };
            return expense.ToList();
        }

        

        //<!-------------------- Notif ------------------------> 
            public int NotifRequest(int expenseid)
        {
            var data = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Request is being Processed <p>");
            sb.Append("<div>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> Reimbursement Team <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $"【Reimbursment】Request Successful";
                    mail.Body = sb.ToString();
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }


        //<!----------------- Notif Finances -------------------> 
        public int NotifApproveF(int expenseid)
        {
            var data = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Have been <b>accepted<b> by Finances <p>");
            sb.Append("<div>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> Finance Department <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $"【Reimbursment】Finances Approve";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }

        public int NotifRejectF(int expenseid)
        {
            var data = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();
           

            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Have been <b>rejected<b> by Finance <p>");
            sb.Append("<div>");
            sb.Append($"<p> Additional message : {data.Expense.CommentFinace} Rejected<p>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> Finance Department <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $"【Reimbursment】Rejected";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }


        //<!----------------- Notif Manager -------------------> 

        public int NotifApproveM(int expenseid)
        {
            var data = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();
      

            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Have been <b>accepted<b> by Manager <p>");
            sb.Append("<div>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> Manager <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $"【Reimbursment】Manager Approve";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }

        public int Update(ExpenseVM expenseVM)
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        where b.ExpenseId == expenseVM.ExpenseId
                        select new { expenses = b }).Single();

            var expense = data.expenses;

            expense.Approver = expenseVM.Approver;
            expense.Purpose = expenseVM.Purpose;
            expense.Description = expenseVM.Description;
            expense.Total = expenseVM.Total;
            switch (expenseVM.Status)
            {
                case 0:
                    expense.Status = Status.Approved;
                    break;
                case 1:
                    expense.Status = Status.Rejected;
                    break;
                case 2:
                    expense.Status = Status.Canceled;
                    break;
                case 3:
                    expense.Status = Status.Posted;
                    break;
                case 4:
                    expense.Status = Status.Draft;
                    break;
                default:
                    break;
            }
            expense.EmployeeId = expenseVM.EmployeeId;
            var expensess = expense;
            context.SaveChanges();
            return 1;
        }

        public int NotifRejectM(int expenseid)
        {
            var data = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();
    
            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Have been <b>rejected<b> at phase 1 <p>");
            sb.Append("<div>");
            sb.Append($"<p> Additional message : {data.Expense.CommentManager} Rejected<p>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> Manager <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $"【Reimbursment】 Rejected";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }


        //<!----------------- Notif Senior Manager ------------------->
        public int NotifApproveSM(int expenseid)
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        where b.ExpenseId == expenseid
                        select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();


            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Have been <b>accepted<b> by Senior Manager <p>");
            sb.Append("<div>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> Senior Manager <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $" 【Reimbursment】Senior Manager Approve ";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }

        public int UpdateSM(ExpenseVM expenseVM)
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        where b.ExpenseId == expenseVM.ExpenseId
                        select new { expenses = b }).Single();

            var expense = data.expenses;

            expense.Approver = expenseVM.Approver;
            expense.Purpose = expenseVM.Purpose;
            expense.Description = expenseVM.Description;
            expense.Total = expenseVM.Total;
            switch (expenseVM.Status)
            {
                case 0:
                    expense.Status = Status.Approved;
                    break;
                case 1:
                    expense.Status = Status.Rejected;
                    break;
                case 2:
                    expense.Status = Status.Canceled;
                    break;
                case 3:
                    expense.Status = Status.Posted;
                    break;
                case 4:
                    expense.Status = Status.Draft;
                    break;
                default:
                    break;
            }
            expense.EmployeeId = expenseVM.EmployeeId;
            var expensess = expense;
            context.SaveChanges();
            return 1;
        }

        public int NotifRejectSM(int expenseid)
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        where b.ExpenseId == expenseid
                        select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Have been <b>rejected<b> by Senior Manager <p>");
            sb.Append("<div>");
            sb.Append($"<p> Additional message : {data.Expense.CommentManager} Rejected<p>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> Senior Manager <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $"【Reimbursment】 Rejected";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }



        //<!----------------- Notif Director ------------------->
        public int NotifApproveD(int expenseid)
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        where b.ExpenseId == expenseid
                        select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();


            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Have been <b>accepted<b> by Director <p>");
            sb.Append("<div>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> Director <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $" 【Reimbursment】Director Approve ";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }

        public int UpdateD(ExpenseVM expenseVM)
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        where b.ExpenseId == expenseVM.ExpenseId
                        select new { expenses = b }).Single();

            var expense = data.expenses;

            expense.Approver = expenseVM.Approver;
            expense.Purpose = expenseVM.Purpose;
            expense.Description = expenseVM.Description;
            expense.Total = expenseVM.Total;
            switch (expenseVM.Status)
            {
                case 0:
                    expense.Status = Status.Approved;
                    break;
                case 1:
                    expense.Status = Status.Rejected;
                    break;
                case 2:
                    expense.Status = Status.Canceled;
                    break;
                case 3:
                    expense.Status = Status.Posted;
                    break;
                case 4:
                    expense.Status = Status.Draft;
                    break;
                default:
                    break;
            }
            expense.EmployeeId = expenseVM.EmployeeId;
            var expensess = expense;
            context.SaveChanges();
            return 1;
        }

        public int NotifRejectD(int expenseid)
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        where b.ExpenseId == expenseid
                        select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append($"<p> Dear {data.Employee.FirstName} {data.Employee.LastName},<br> Your Reimbursement Have been <b>rejected<b> by Director <p>");
            sb.Append("<div>");
            sb.Append($"<p> Additional message : {data.Expense.CommentManager} Rejected<p>");
            sb.Append(" You have made a Reimbursment Request with ID ");
            sb.Append($"<h1> # {expenseid} <h1>");
            sb.Append("<h4> Best Regards, <h4>");
            sb.Append("<h4> DIrector <h4>");

            if (data != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(data.Employee.Email);
                    mail.Subject = $"【Reimbursment】 Rejected";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }
    }
}