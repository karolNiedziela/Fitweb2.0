using Fitweb.Domain.Exceptions;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.Exceptions;
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
                throw new InvalidCredentialsException();
            }

            return user;
        }

        public static async Task<User> FindByEmailOrFailAsync(this UserManager<User> userManager, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            return user;
        }
    }
}
