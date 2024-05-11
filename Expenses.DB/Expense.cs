using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.DB
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
