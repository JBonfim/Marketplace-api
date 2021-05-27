using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SMarketplace.API.Configuration;
using SMarketplace.API.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMarketplace.API
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);
            // services.AddHangfireConfiguration(Configuration);

            services.AddApiConfiguration();

            services.AddSwaggerConfiguration();

            

            services.AddMediatR(typeof(Startup));

            services.RegisterServices(Configuration);


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(env);
            // app.UseHangfireConfiguration(env, serviceProvider);
        }
    }
}
