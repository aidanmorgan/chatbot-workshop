using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DDDPerthBot.Persistence;
using DDDPerthBot.Persistence.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace DDDPerthBot.Services.Api
{
    public class Startup
    {
        private readonly string _basePath;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _basePath = env.ContentRootPath;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DDD Perth Bot API", Version = "v1" });
            });

            services.AddSingleton<IUnitOfWork>((x) =>
            {
                return new JsonUnitOfWork(LoadFile("speakers.json"), LoadFile("sessions.json"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD Perth Bot API");
            });

        }

        private string LoadFile(string speakersJson)
        {
            var pathToFile = _basePath
                             + Path.DirectorySeparatorChar.ToString()
                             + "Data"
                             + Path.DirectorySeparatorChar.ToString()
                             + speakersJson;

            string fileContent;

            using (StreamReader reader = File.OpenText(pathToFile))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent;
        }
    }
}
