
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using SMarketplace.Application.Commands;
using SMarketplace.Application.Services;
using SMarketplace.Core.Data;
using SMarketplace.Core.Mediator;
using SMarketplace.Data.Repository;
using SMarketplace.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SMarketplace.API.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            #region Commands e Events
            //// Domain - Commands
             services.AddScoped<IRequestHandler<RegisterProductCommand, ValidationResult>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, ValidationResult>, ProductCommandHandler>();
            









            //// Domain - Events
            // services.AddScoped<INotificationHandler<ApiRegisteredEvent>, ApiEventHandler>();


            #endregion




            #region Services

            ////Services
            services.AddHttpClient<IIdentityService, IdentityService>()
              .AddPolicyHandler(PollyExtensions.EsperarTentar())
              .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

           
            services.AddScoped<IProductService, ProductService>();

            #endregion


            #region Repository

            ////Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));






            #endregion



            #region Application
            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            #endregion







        }

        #region PollyExtension
        public static class PollyExtensions
        {
            public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
            {
                var retry = HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(new[]
                    {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    }, (outcome, timespan, retryCount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Tentando pela {retryCount} vez!");
                        Console.ForegroundColor = ConsoleColor.White;
                    });
                return retry;
            }
        }
        #endregion
    }
}
