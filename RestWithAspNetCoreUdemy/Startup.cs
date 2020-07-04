using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestWithAspNetCoreUdemy.Hypermedia;
using RestWithAspNetCoreUdemy.Models.Context;
using RestWithAspNetCoreUdemy.Repository.Concretes;
using RestWithAspNetCoreUdemy.Repository.Generic;
using RestWithAspNetCoreUdemy.Repository.Interfaces;
using RestWithAspNetCoreUdemy.Services.Concretes;
using RestWithAspNetCoreUdemy.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using Tapioca.HATEOAS;

namespace RestWithAspNetCoreUdemy
{
    public class Startup
    {

        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(opt => opt.UseMySql(connectionString));

            // configuração de migrations
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                {
                    Locations = new List<string> { "db/migrations" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }   
            catch(Exception ex)
            {
                _logger.LogCritical("Database migration failed", ex);
                throw;
            }

            // Add framework services.
            services.AddMvc();

            // Versionamento (Microsoft.Extensions.DependencyInjection)
            services.AddApiVersioning();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "RESTful API With ASP.NET Core",
                        Version = "v1"
                    });
            });

            // HATEOS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);

            // DI
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            #region "ConfiguracaoSwagger"
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
            #endregion

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                );
            });
        }
    }
}
