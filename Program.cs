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

            string connectString = builder.Configuration.GetConnectionString("MyBlogContext") ?? throw new Exception("Ket noi ko thanh cong");
            builder.Services.AddDbContext<MyBlogContext>(options =>
            {
                options.UseSqlServer(connectString);
            });

            builder.Services.AddOptions();
            var mailSettings = builder.Configuration.GetSection("MailSettings");
            builder.Services.Configure<MailSettings>(mailSettings);

            builder.Services.AddSingleton<IEmailSender, SendMailService>();

            // Identity
            //builder.Services.AddIdentity<AppUser, IdentityRole>()
            //    .AddEntityFrameworkStores<MyBlogContext>()
            //    .AddDefaultTokenProviders();

            builder.Services.AddDefaultIdentity<IdentityUser>()
                  .AddEntityFrameworkStores<MyBlogContext>()
                  .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 1;

                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = true;

                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/not-accessdible";
            });


            builder.Services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        var googleConfig = builder.Configuration?.GetSection("Authentication:Google") ?? throw new Exception("Khong tim thay cau hinh google");
                        options.ClientId = googleConfig["ClientId"];
                        options.ClientSecret = googleConfig["ClientSecret"];
                        // https://localhost:7139/signin-google
                        options.CallbackPath = "/login-google";
                    })
                    .AddFacebook(options =>
                    {
                        IConfigurationSection fConfig = builder.Configuration?.GetSection("Authentication:Facebook") ?? throw new Exception("Khong tim thay cau hinh google");
                        options.ClientId = fConfig["ClientId"];
                        options.ClientSecret = fConfig["ClientSecret"];
                        options.CallbackPath = "/login-facebook";
                    })
                    //.AddTwitter()
                    //.AddMicrosoftAccount()
                    ;



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
        - Authentication: Xac dinh danh tinh -> login, logout...
        - Authorization: Xac thuc quyen truy cap
        - Quan ly user: Sign up, User, Role....
 
    IdentityUser user;
    IdentityDbContxt context;

    /Identity/Account/Login
    /Identity/Account/Manager


    // https://localhost:7139/login-google callback Path

 */ 