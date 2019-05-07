using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.UTC.Core.FakeServices;
using Altkom.UTC.Core.FakeServices.Fakers;
using Altkom.UTC.Core.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;

namespace Altkom.UTC.Core.Service
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)                
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddXmlFile("appsettings.xml")
                ;

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDevicesService, FakeDevicesService>();
            services.AddSingleton<ICustomersService, FakeCustomersService>();
            services.AddSingleton<DeviceFaker>();
            services.AddSingleton<CustomerFaker>();

            var quantity = Configuration["Quantity"];

            var option2 = Configuration["MyComplexOptions:MyOption2"];

            string connectionString = Configuration.GetConnectionString("MyConnection");

            // services.AddScoped<IDevicesService, DbDevicesService>();

            // dotnet add package Microsoft.Extensions.Configuration.Yaml

            services
                .AddMvc(options => options.RespectBrowserAcceptHeader = true)
                .AddXmlSerializerFormatters()
                .AddJsonOptions(options =>
                   {
                       options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));
                   })
                //.AddYamlFile("appsettings.yml")
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "My Api", Version = "1.0" }));

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }

        public void ConfigureProduction(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
