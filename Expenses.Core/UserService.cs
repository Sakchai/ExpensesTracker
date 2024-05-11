using System;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Core.CustomExceptions;
using Expenses.Core.DTO;
using Expenses.Core.Utilities;
using Expenses.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Core
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(AppDbContext context,
            IPasswordService passwordService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _passwordService = passwordService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var userToSignIn = await _userManager.FindByEmailAsync(user.Email);

            if (userToSignIn == null)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(userToSignIn, user.Password, false, false);

            return new AuthenticatedUser()
            {
                Username = user.UserName,
                Token = JwtGenerator.GenerateAuthToken(user.UserName),
            };
        }

        public async Task<AuthenticatedUser> ExternalSignIn(User user)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.ExternalId.Equals(user.ExternalId) && u.ExternalType.Equals(user.ExternalType));

            if (dbUser == null)
            {
                user.UserName = CreateUniqueUsernameFromEmail(user.Email);
                return await SignUp(user);
            }

            return new AuthenticatedUser()
            {
                Username = dbUser.UserName,
                Token = JwtGenerator.GenerateAuthToken(dbUser.UserName),
            };
        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUsername = await _userManager.FindByEmailAsync(user.UserName);

            if (checkUsername != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = _passwordService.HashPassword(user.Password);
            }

            var result = await _userManager.CreateAsync(user);

            return new AuthenticatedUser
            {
                Username = user.UserName,
                Token = JwtGenerator.GenerateAuthToken(user.UserName)
            };
        }

        private string CreateUniqueUsernameFromEmail(string email)
        {
            var emailSplit = email.Split('@').First();
            var random = new Random();
            var username = emailSplit;

            while (_context.Users.Any(u => u.UserName.Equals(username)))
            {
                username = emailSplit + random.Next(10000000);
            }

            return username;
        }
    }
}
