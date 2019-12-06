using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Injection de dépendances
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<ILoginDB, LoginDB>();

            services.AddScoped<IRestaurantsDB, RestaurantsDB>();
            services.AddScoped<IRestaurantsManager, RestaurantsManager>();

            services.AddScoped<ICitiesManager, CitiesManager>();
            services.AddScoped<ICitiesDB, CitiesDB>();

            services.AddScoped<IOrderDishesManager, OrderDishesManager>();
            services.AddScoped<IOrderDishesDB, OrderDishesDB>();

            services.AddScoped<IOrdersStatusHistoryManager, OrdersStatusHistoryManager>();
            services.AddScoped<IOrdersStatusHistoryDB, OrdersStatusHistoryDB>();

            services.AddScoped<IOrdersStatusManager, OrdersStatusManager>();
            services.AddScoped<IOrdersStatusDB, OrdersStatusDB>();

            services.AddScoped<IOrdersDB, OrdersDB>();
            services.AddScoped<IOrdersManager, OrdersManager>();

            services.AddScoped<ICustomersManager, CustomersManager>();
            services.AddScoped<ICustomersDB, CustomersDB>();

            services.AddScoped<IDeliverersManager, DeliverersManager>();
            services.AddScoped<IDeliverersDB, DeliverersDB>();

            services.AddScoped<IDishesManager, DishesManager>();
            services.AddScoped<IDishesDB, DishesDB>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession(); // Pour utiliser les sessions

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
