using Expenses.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Core
{
    public class StatisticsServices : IStatisticsServices
    {
        private readonly DB.AppDbContext _context;
        private readonly DB.User _user;
        private readonly UserManager<User> _userManager;

        public StatisticsServices(DB.AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            var httpContext = httpContextAccessor.HttpContext;
            var userEmail = httpContext.Request.Headers["Email"].FirstOrDefault()?.ToString();

            _user = _userManager.FindByEmailAsync(userEmail).ConfigureAwait(false).GetAwaiter().GetResult();

        }

        public IEnumerable<KeyValuePair<string, double>> GetExpenseAmountPerCategory()
        {
            if (_user != null)
            {
                return _context.Expenses
                    .Where(e => e.User.Id == _user.Id)
                    .AsEnumerable()
                    .GroupBy(e => e.Description)
                    .ToDictionary(e => e.Key, e => e.Sum(x => x.Amount))
                    .Select(x => new KeyValuePair<string, double>(x.Key, x.Value));
            }
            else
            {
                return new List<KeyValuePair<string, double>>();
            }


        }
    }
}
