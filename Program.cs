using ASP_RAZOR_5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ASP_RAZOR_5.Services;


namespace ASP_RAZOR_5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // hhelo
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MyBlogContext>(options =>
            {
                string connectString = builder.Configuration.GetConnectionString("MyBlogContext");
                options.UseSqlServer(connectString);
            });

            // Dang ky Identity
            //builder.Services.AddIdentity<AppUser, IdentityRole>()
            //        .AddEntityFrameworkStores<MyBlogContext>()
            //        .AddDefaultTokenProviders();
            builder.Services.AddDefaultIdentity<IdentityUser>()
                     .AddEntityFrameworkStores<MyBlogContext>()
                     .AddDefaultTokenProviders();
            builder.Services.AddOptions();

            var mailSetting = builder.Configuration.GetSection("MailSettings");
            builder.Services.Configure<MailSettings>(mailSetting);
            builder.Services.AddSingleton<IEmailSender, SendMailService>();

            //var mailSettings = Configuration.GetSection("MailSettings");

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}

/**
CREATE, READ, UPDATE, DELETE (CRUD)
    dotnet aspnet-codegenerator razorpage -m ASP_RAZOR_5.Models.Article -dc ASP_RAZOR_5.Models.MyBlogContext -outDir Pages/Blog -udl --referenceScriptLibraries
    dotnet aspnet-codegenerator identity -dc ASP_RAZOR_5.Models.MyBlogContext



    Identity:
        - authentication: xac dinh danh tinh -> login logout....
        - Authorization: xac dinh quyen truy cap
        - Quan ly user: Sign UP, User, Role ....
            
        IdentityUser user;
        IdentityDbContext context;
        SignInManager<AppUser> s;
        UserManager<AppUser> u;

    /Identity/Account/Login
    /Identity/Account/Manage

 */ 