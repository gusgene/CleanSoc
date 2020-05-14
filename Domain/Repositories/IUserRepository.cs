// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public interface IUserRepository
    {
        Task<bool> EmailExistInDb(string email);
        Task<bool> UserNameExistInDb(string userName);

        Task<bool> Add(AppUser user, string password);
    }
}
