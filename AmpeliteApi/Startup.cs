using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmpeliteApi.Data;
using AmpeliteApi.Services.SalePromotion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AmpeliteApi
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

            services.AddDbContext<db_AmpeliteContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AmpeliteConnection")));

            services.AddDbContext<db_AmpelwebContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AmpelwebConnection")));

            services.AddMvc();

            services.AddTransient<ICodePromotionService, CodePromotionService>();
            services.AddTransient<IPromotionTargetService, PromotionTargetService>();
            services.AddTransient<IGetTransactionInvService, GetTransactionInvService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            }


            app.UseMvc();
        }
    }
}