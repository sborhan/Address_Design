using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Address_Design.Data;
using Address_Design.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi.Models;


namespace Address_Design
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
            services.AddControllersWithViews();

            services.AddServerSideBlazor();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("AddressForm", new OpenApiInfo {Title = "AddressForm API", Version = "v1"});

            });

            // This is just got ToDoContext to work, can pre removed when SQL server is in place
            services.AddDbContext<Address_DesignContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Address_DesignContext")));

            services.AddMvc();
            services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Views");

            // This is just got ToDoContext to work, can pre removed when SQL server is in place
            services.AddDbContext<ToDoContext>(options =>
                options.UseInMemoryDatabase("db"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/AddressForm/swagger.json", "AddressForm");
                    options.RoutePrefix = string.Empty;
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AddressForm}/{action=Index}/{id?}");

                endpoints.MapBlazorHub();

                endpoints.MapFallbackToPage("/AddressForm/_Host");
            });

        }
    }
}
