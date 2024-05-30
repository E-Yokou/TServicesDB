using Microsoft.EntityFrameworkCore;
using TServicesDB.Models;
using Microsoft.AspNetCore.Identity;
using TServicesDB.Data;
using TServicesDB.Areas.Identity.Data;

namespace TServicesDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<TServicesDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TServicesDB")));

            builder.Services.AddDbContext<TServicesDBUserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TServicesDBConnection")));

            builder.Services.AddIdentity<TServicesDBUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                 .AddEntityFrameworkStores<TServicesDBUserContext>()
                 .AddUserManager<UserManager<TServicesDBUser>>();

            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.AccessDeniedPath = new PathString("/Identity/Account/AccessDenied");
                opt.LoginPath = new PathString("/Identity/Account/Login");
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}