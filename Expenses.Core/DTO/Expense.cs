using System;

namespace Expenses.Core.DTO
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        public static explicit operator Expense(DB.Expense e) => new Expense
        {
            Id = e.Id,
            Description = e.Description,
            Amount = e.Amount
        };
    }
}
