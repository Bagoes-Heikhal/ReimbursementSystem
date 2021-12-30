﻿using API.Hash;
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
                switch (expenseVM.Status)
                {
                    case 1:
                        expense.Status = Status.Approved;
                        break;
                    case 2:
                        expense.Status = Status.Rejected;
                        break;
                    case 3:
                        expense.Status = Status.Canceled;
                        break;
                    case 4:
                        expense.Status = Status.Posted;
                        break;
                    case 5:
                        expense.Status = Status.Posted;
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
            var register = from a in context.Employees
                           where a.EmployeeId == employeeid
                           join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                           select new ExpenseVM()
                           {
                               DateTime = DateTime.Now,
                               ExpenseId = b.ExpenseId,
                               Purpose = b.Purpose,
                               CommentFinace = b.CommentFinace,
                               CommentManager = b.CommentManager,
                               Status = (int)b.Status,
                               Total = b.Total,
                               Description = b.Description,
                           };
            return register.ToList();
        }

        public IEnumerable<ExpenseManager> GetExpenseModified(string employeeid)
        {
            var expense = from a in context.Employees
                       
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where a.EmployeeId == employeeid && b.Status == Status.Posted
                          select new ExpenseManager()
                           {
                               //DateTime = DateTime.Now,
                               //ExpenseId = b.ExpenseId,
                               //Status = 5,
                               //Total = b.Total,
                               //Description = b.Description,

                               EmployeeId = b.EmployeeId,
                               FirstName = a.FirstName,
                               DateTime = DateTime.Now,
                               Total = b.Total,
                               Description = b.Description,



                           };
            return expense.ToList();
        }

        //<!----------------- Finances -------------------> 
        public IEnumerable<ExpenseManager> GetExpenseFinance()
        {
            var expense = from a in context.Employees
                          join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                          where b.Status == Status.ApprovedByManager
                          select new ExpenseManager()
                          {
                              Status = (int)b.Status,
                              EmployeeId = b.EmployeeId,
                              ExpenseId = b.ExpenseId,
                              Name = a.FirstName + " " + a.LastName,
                              DateTime = DateTime.Now,
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
                              DateTime = DateTime.Now,
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
                              DateTime = DateTime.Now,
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
                              DateTime = DateTime.Now,
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
                              DateTime = DateTime.Now,
                              Total = b.Total,
                              Description = b.Description,
                              Purpose = b.Purpose
                          };
            return expense.ToList();
        }
        public int NonSessionSubmit(ExpenseVM expenseVM)
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


        //<!-------------------- Notif ------------------------> 
        public int NotifRequest(string email, int expenseid)
        {
            var OlddPass = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where a.Email == email && b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p> You have made a Reimbursment Request with <p>");
            sb.Append("<p> Your Request Id is  <p>");
            sb.Append($"<h1> # {expenseid} <h1>");

            if (OlddPass != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = $"Reimbursment {DateTime.Now}";
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
        public int NotifApproveF(string email, int expenseid)
        {
            var OlddPass = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where a.Email == email && b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p> Your Reimbursment Have been approved by Finances <p>");
            sb.Append("<p> Your Request Id is  <p>");
            sb.Append($"<h1> # {expenseid} approved  <h1>");

            if (OlddPass != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = $"Finances Approve {DateTime.Now}";
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

        public int NotifRejectF(string email, int expenseid)
        {
            var OlddPass = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where a.Email == email && b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p> Your Reimbursment Have been rejected by Finances<p>");
            sb.Append("<p> Your Request Id is  <p>");
            sb.Append($"<h1> # {expenseid} Rejected<h1>");

            if (OlddPass != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = $"Reject Finances {DateTime.Now}";
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

        public int NotifApproveM(string email, int expenseid)
        {
            var OlddPass = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where a.Email == email && b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p> Your Reimbursment Have been approved by Manager <p>");
            sb.Append("<p> Your Request Id is  <p>");
            sb.Append($"<h1> # {expenseid} approved  <h1>");

            if (OlddPass != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = $"Manager Approve {DateTime.Now}";
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
    

        public int NotifRejectM(string email, int expenseid)
        {
            var OlddPass = (from a in context.Employees
                            join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                            where a.Email == email && b.ExpenseId == expenseid
                            select new { Employee = a, Expense = b }).Single();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p> Your Reimbursment Have been rejected by Manager<p>");
            sb.Append("<p> Your Request Id is  <p>");
            sb.Append($"<h1> # {expenseid} Rejected<h1>");

            if (OlddPass != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = $"Manager Reject {DateTime.Now}";
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