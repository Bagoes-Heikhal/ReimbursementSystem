﻿using API.Hash;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
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
                               dateTime = DateTime.Now,
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
    }
}