using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.UTC.Core.DbServices;
using Altkom.UTC.Core.FakeServices;
using Altkom.UTC.Core.FakeServices.Fakers;
using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Service.Handlers;
using Altkom.UTC.Core.Service.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //services.AddSingleton<IDevicesService, FakeDevicesService>();
            //services.AddSingleton<ICustomersService, FakeCustomersService>();

            services.AddScoped<IDevicesService, DbDevicesService>();
            services.AddScoped<ICustomersService, DbCustomersService>();
            services.AddScoped<IUsersService, DbUsersService>();
            services.AddSingleton<DeviceFaker>();
            services.AddSingleton<CustomerFaker>();

            services.AddAuthentication("BasicAuthorization")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthorization", null);


            var quantity = Configuration["Quantity"];

            var option2 = Configuration["MyComplexOptions:MyOption2"];

            string connectionString = Configuration.GetConnectionString("MyConnection");
        
            // dotnet add package Microsoft.EntityFrameworkCore
            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            services.AddDbContext<UTCContext>(options => options.UseSqlServer(connectionString));

            // services.AddScoped<IDevicesService, DbDevicesService>();

            // Wstrzykiwanie poprzez IOptions<T>

            // services.AddOptions();
            // services.Configure<MyOptions>(Configuration.GetSection("MyOptions"));


            // Bezposrednie wstrzykiwanie T
            var config = new MyOptions();
            Configuration.GetSection("MyOptions").Bind(config);
            services.AddSingleton(config);

            // dotnet add package Microsoft.Extensions.Configuration.Yaml

            services
                .AddMvc(options => options.RespectBrowserAcceptHeader = true)
                .AddXmlSerializerFormatters()
                .AddJsonOptions(options =>
                   {
                       options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));

                       // skip null values
                       options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;


                   })
                //.AddYamlFile("appsettings.yml")
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "My Api", Version = "1.0" }));


            services
                .AddHealthChecks()
                    .AddCheck<RandomHealthCheck>("random");

            services
                .AddHealthChecksUI();

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

            // app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }

        public void ConfigureProduction(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
