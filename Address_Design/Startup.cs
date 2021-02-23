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
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            //services.AddSwaggerGen(
            //   options =>
            //   {
            //       OpenApiInfo apiInfo = new OpenApiInfo()
            //       {
            //           Title = "Azure Security KubernetesOrchestrator",
            //           Description = "v1",
            //           Version = "v1",
            //       };

            //       OpenApiSecurityScheme scheme = new OpenApiSecurityScheme()
            //       {
            //           In = ParameterLocation.Header,
            //           Type = SecuritySchemeType.ApiKey,
            //           Description = "Please insert JWT with Bearer into field",
            //           Name = "Authorization",
            //       };

            //       options.AddSecurityDefinition("Bearer", scheme);
            //       options.SwaggerDoc("v1", apiInfo);
            //   });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Address");
                    options.RoutePrefix = string.Empty;
                });
        }
    }
}
