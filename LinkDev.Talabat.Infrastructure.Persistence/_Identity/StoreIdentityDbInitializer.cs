using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    internal sealed class StoreIdentityDbInitializer(StoreIdentityContext _dbContext , UserManager<ApplicationUser> _userManager) : DbInitializer(_dbContext) , IStoreIdentityDbInitializer
    {

        public override async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Ahmed Nasr",
                    UserName = "ahmed.nasr",
                    Email = "ahmed.nasr@linkdev.com",
                    PhoneNumber = "01234567890"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd");  
            }
        }
    }
}
