using API.Hash;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public int LastExpense()
        {
            var data = (from a in context.Employees
                        join b in context.Expenses on a.EmployeeId equals b.EmployeeId
                        select new { id = b.ExpenseId}).LastOrDefault();

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
                           select new ExpenseVM()
                           {
                               DateTime = DateTime.Now,
                               ExpenseId = b.ExpenseId,
                               Status = 5,
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

        //public IEnumerable<ExpenseVM> ExpenseAllData()
        //{
        //    var data = from a in context.Employees
        //                   where a.EmployeeId == employeeid
        //                   join b in context.Expenses on a.EmployeeId equals b.EmployeeId
        //                   select new ExpenseVM()
        //                   {
        //                       dateTime = DateTime.Now,
        //                       ExpenseId = b.ExpenseId,
        //                       Status = 5,
        //                       Total = b.Total,
        //                       Description = b.Description,
        //                   };
        //    return data.ToList();
        //}

        //public IEnumerable GetAllExpense()
        //{
        //    var data = from e in context.Set<Expense>()
        //                select new

        //                {
        //                    e.EmployeeId,
        //                    e.Status,
        //                    e.Approver,
        //                    e.Description,
        //                    e.Comment,
        //                    e.Total

        //                };
        //    return data.ToList();
        //}

    }
}
