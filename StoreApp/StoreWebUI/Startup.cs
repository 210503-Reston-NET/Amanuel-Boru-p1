using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreDL;
using StoreBL;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace StoreWebUI
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
            Log.Logger = new LoggerConfiguration()
                           .WriteTo.AzureBlobStorage(Configuration.GetConnectionString("Logging"))
                           .CreateLogger();

            services.AddControllersWithViews();
            services.AddDbContext<StoreDBContext>(options => options.UseNpgsql(Configuration.GetConnectionString("StoreDB")));
            services.AddScoped<CustomerDB>();
            services.AddScoped<ManagerDB>();
            services.AddScoped<ProductsDB>();
            services.AddScoped<LocationDB>();
            services.AddScoped<OrderDB>();
            services.AddScoped<ICustomerBL, CustomerBL>();
            services.AddScoped<ManagerBL>();
            services.AddScoped<IProductBL, ProductBL>();
            services.AddScoped<ILocationBL, LocationBL>();
            services.AddScoped<IOrderBL, OrderBL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
