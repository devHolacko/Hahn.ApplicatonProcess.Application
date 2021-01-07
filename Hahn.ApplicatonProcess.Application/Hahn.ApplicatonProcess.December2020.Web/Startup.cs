using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Data.Services.ApplicantService;
using Hahn.ApplicatonProcess.December2020.Data.Services.CountryService;
using Hahn.ApplicatonProcess.December2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.December2020.Web.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace Hahn.ApplicatonProcess.December2020.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicantDbContext>(options => options.UseInMemoryDatabase(Configuration["DatabaseName"]));
            services.AddScoped(typeof(IRepository<>), typeof(ApplicantRepository<>));
            services.AddTransient<IApplicantDataService, ApplicantDataService>();
            services.AddSingleton<ICountryDataService>(x => new CountryDataService(Configuration["CountriesBaseUrl"]));
            services.AddAutoMapper(typeof(ApplicantMapper));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Applicant API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
