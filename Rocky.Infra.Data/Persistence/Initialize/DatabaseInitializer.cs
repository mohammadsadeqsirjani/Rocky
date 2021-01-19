using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rocky.Application.Utilities;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Scrutor;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Infra.Data.Persistence.Initialize
{
    public class DatabaseInitializer : IScoped, IDatabaseInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        void IDatabaseInitializer.Initialize()
        {
            try
            {
                if ((_db.Database.GetPendingMigrations()).Any()) _db.Database.Migrate();
            }
            catch (Exception)
            {
                //ignore
            }

            if (_roleManager.RoleExistsAsync(WebConstant.AdminRole).GetAwaiter().GetResult())
                return;

            _roleManager.CreateAsync(new IdentityRole(WebConstant.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(WebConstant.CustomerRole)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FullName = "Administrator",
                PhoneNumber = "1112221111",

            }, "Sa@12345678").GetAwaiter().GetResult();

            var user = _db.ApplicationUser.FirstOrDefault(x => x.Email == "admin@gmail.com");

            _userManager.AddToRoleAsync(user, WebConstant.AdminRole).GetAwaiter().GetResult();
        }

        public async Task InitializeAsync()
        {
            try
            {
                if ((await _db.Database.GetPendingMigrationsAsync()).Any()) await _db.Database.MigrateAsync();
            }
            catch (Exception)
            {
                //ignore
            }

            if (await _roleManager.RoleExistsAsync(WebConstant.AdminRole))
                return;

            await _roleManager.CreateAsync(new IdentityRole(WebConstant.AdminRole));
            await _roleManager.CreateAsync(new IdentityRole(WebConstant.CustomerRole));

            await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FullName = "Administrator",
                PhoneNumber = "1112221111",

            }, "Sa@12345678");

            var user = _db.ApplicationUser.FirstOrDefault(x => x.Email == "admin@gmail.com");

            await _userManager.AddToRoleAsync(user, WebConstant.AdminRole);
        }
    }
}
