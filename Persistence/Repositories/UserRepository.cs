// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Domain;
    using Domain.Repositories;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> EmailExistInDb(string email)
        {
            var result = await _context.Users.Where(x => x.Email == email).AnyAsync();
            return result;
        }

        public async Task<bool> UserNameExistInDb(string userName)
        {
            var result = await _context.Users.Where(x => x.UserName == userName).AnyAsync();
            return result;
        }

        public async Task<bool> Add(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }
    }
}
