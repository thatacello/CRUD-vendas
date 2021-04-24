using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
           services.AddRazorPages();
            // ------------------ seeding service-----------------------
           services.AddScoped<SeedingService>();
           // ----------------------------------------------------------
           services.AddScoped<SellerService>();
           services.AddScoped<DepartmentService>();
           services.AddScoped<SalesRecordService>();
           // ----------------------------------------------------------
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, SeedingService seedingService)
        {
            // ------------ podrão estados unidos ------------
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo>{ enUS },
                SupportedUICultures = new List<CultureInfo>{ enUS }
            };
            app.UseRequestLocalization(localizationOptions);
            // ------------------------------------------------
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                seedingService.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // ? -> significa que o "id" é opcional
                endpoints.MapRazorPages();
            });

            // foi realizada a injeção de dependência do IServiceProvider 
            // object migrate cria o banco de dados utilizando o migrations ao subir a aplicação,
            // sem a necessidade de criar o banco de dados manualmente
            serviceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
