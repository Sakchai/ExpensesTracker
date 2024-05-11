using Expenses.Core.DTO;
using System.Collections.Generic;

namespace Expenses.Core
{
    public interface IExpensesServices
    {
        List<Expense> GetExpenses();
        Expense GetExpense(int id);
        Expense CreateExpense(DB.Expense expense);
        void DeleteExpense(Expense expense);
        Expense EditExpense(Expense expense);
    }
}
