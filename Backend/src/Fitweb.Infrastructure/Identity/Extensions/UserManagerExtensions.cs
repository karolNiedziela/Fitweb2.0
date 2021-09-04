using Fitweb.Application.Exceptions;
using Fitweb.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<User> FindByNameOrFailAsync(this UserManager<User> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user is null)
            {
                // TODO: Probably should be invalid credentials
                throw new NotFoundException(nameof(User), username, KeyType.Username);
            }

            return user;
        }

        public static async Task<User> FindByEmailOrFailAsync(this UserManager<User> userManager, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                // TODO: Probably should be invalid credentials
                throw new NotFoundException(nameof(User), email);
            }

            return user;
        }
    }
}
