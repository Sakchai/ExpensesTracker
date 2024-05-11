using System.Collections.Generic;
using System.Linq;
using Expenses.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Expenses.Core
{
    public class ExpensesServices : IExpensesServices
    {
        private readonly DB.AppDbContext _context;
        private readonly DB.User _user;
        private readonly UserManager<User> _userManager;
        public ExpensesServices(DB.AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> _userManager)
        {
            _context = context;
            var httpContext = httpContextAccessor.HttpContext;
            var userEmail = httpContext.Request.Headers["Email"].FirstOrDefault()?.ToString();

            _user = _userManager.FindByEmailAsync(userEmail).ConfigureAwait(false).GetAwaiter().GetResult();

        }

        public DTO.Expense CreateExpense(DB.Expense expense)
        {
            if (_user != null)
            {
                expense.User = _user;
                _context.Add(expense);
                _context.SaveChanges();
            }
            return (DTO.Expense)expense;

        }

        public void DeleteExpense(DTO.Expense expense)
        {
            if (_user != null)
            {
                var dbExpense = _context.Expenses.First(e => e.User.Id == _user.Id && e.Id == expense.Id);
                _context.Remove(dbExpense);
                _context.SaveChanges();
            }
        }

        public DTO.Expense EditExpense(DTO.Expense expense)
        {
            if (_user != null)
            {
                var dbExpense = _context.Expenses
                 .Where(e => e.User.Id == _user.Id && e.Id == expense.Id)
                 .First();
                dbExpense.Description = expense.Description;
                dbExpense.Amount = expense.Amount;
                _context.SaveChanges();
            }
            return expense;
        }


        public DTO.Expense GetExpense(int id)
        {
            if (_user != null)
            {
                return _context.Expenses
                .Where(e => e.User.Id == _user.Id && e.Id == id)
                .Select(e => (DTO.Expense)e)
                .First();
            }
            else
            { return new DTO.Expense(); }
        }

        public List<DTO.Expense> GetExpenses()
        {
            if (_user != null)
            {
                return _context.Expenses
                .Where(e => e.User.Id == _user.Id)
                .Select(e => (DTO.Expense)e)
                .ToList();
            }
            else
            { return new List<DTO.Expense>(); }
        }

    }
}
