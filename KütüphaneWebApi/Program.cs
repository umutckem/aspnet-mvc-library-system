using KütüphaneWebApi.Data;
using KütüphaneWebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace KütüphaneWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Veritabanı bağlantı dizesi
            var conStr = builder.Configuration
                .GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(conStr));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Servisleri ekleme
            builder.Services.AddScoped<IKitapServices, KitapServices>();

            builder.Services.AddScoped<IDestekServices,DestekServices>();
            
            builder.Services.AddCustomIdentity();
            builder.Services.ConfigureLocalization();
            builder.Services.ConfigureApplicationCookie(options =>
            {

                options.ExpireTimeSpan = TimeSpan.FromHours(12);
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = false;
            });

            builder.Services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization()
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "Bu alan boş bırakılamaz.");
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, fieldName) => $"{fieldName} geçerli bir değer değil.");
                    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(fieldName => $"{fieldName} gerekli bir alandır.");
                    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Bu alan gerekli.");
                    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(value => "Geçersiz bir değer girildi.");
                    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(fieldName => $"{fieldName} sayısal bir değer olmalıdır.");
                });

            var app = builder.Build();

            // Hata yönetimi
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Yerelleştirme ayarları
            var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            app.UseAuthentication();
            app.UseAuthorization();

            // Routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Kitaps}/{action=Index}/{id?}");
            });

            app.MapRazorPages();
            app.Run();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 10;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection ConfigureLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            var supportedCultures = new[] { new CultureInfo("tr"), new CultureInfo("en") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("tr");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            return services;
        }
    }
}
