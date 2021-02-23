using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Contracts;
using TopeyPay.Domain;
using TopeyPay.Domain.IServices;
using TopeyPay.Http.Interfaces;
using TopeyPay.Http.Services;
using TopeyPay.Infrastructure;
using TopeyPay.Infrastructure.Services;
using TopeyPay.Services;
using TopeyPay.Shared.Contracts;
using TopeyPay.Shared.Services;

namespace TopeyPay.API
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
            services.AddDbContext<TopeyPayContext>(opt => opt.UseSqlite(
             Configuration.GetConnectionString("DbConnection")));
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TopeyPay.API", Version = "v1" });
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILogWriter, LogWriter>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentStatusService, PaymentStatusService>();

            services.AddScoped<IProcessPayment, ProcessPayment>();
            //register external services
            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<IPremiumPaymentGateway, PremiumPaymentGateway>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TopeyPay.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
