using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Expenses.DB
{
    public class User : IdentityUser
    {
        public string Password { get; set; }   
        public string ExternalId { get; set; }
        public string ExternalType { get; set; }
    }
}
