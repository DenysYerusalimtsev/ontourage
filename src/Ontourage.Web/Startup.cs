using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ontourage.Core.Email;
using Ontourage.Core.Interfaces;
using Ontourage.DataAccess.SqlServer;

namespace Ontourage.Web
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
            services.AddMvc();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<IDbConnection>(provider => new SqlConnection(connectionString));
            services.AddTransient<IDbConnectionFactory>(provider => new SqlConnectionFactory(connectionString));
            services.AddTransient<IVoucherRepository, DbVoucherRepository>();
            services.AddTransient<IFoodTypeRepository, DbFoodTypesRepository>();
            services.AddTransient<ICountryRepository, DbCountryRepository>();
            services.AddTransient<IHotelRepository, DbHotelRepository>();
            services.AddTransient<ITourOperatorRepository, DbTourOperatorRepository>();
            services.AddTransient<IPaymentChecksRepository, DbPaymentChecksRepository>();
            services.AddTransient<IClientRepository, DbClientRepository>();
            services.AddTransient<IDiscountRepository, DbDiscountRepository>();
            services.AddTransient<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
