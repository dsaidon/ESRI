using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Dudi",
                    Email = "Dudi@gmail.com",
                    UserName = "dsaidon",
                    Address = new Address
                    {
                        FirstName = "Dudi",
                        LastName = "Saidon",
                        Street = "Duchifat 3",
                        City = "Netanya",
                        State = "ISRAEL",
                        ZipCode = "90210"
                    }
                };
                await userManager.CreateAsync(user, "DSaidon1$");
            }
        }
    }
}
