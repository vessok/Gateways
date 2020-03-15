using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Gateways.Persistance;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using Gateways.Services;
using AutoMapper;

namespace Gateways {
    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddSwaggerGen(cfg => {
                cfg.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Getways",
                    Version = "v1",
                    Description = "Demo REST API built with ASP.NET Core 3.1.",
                    Contact = new OpenApiContact { Name = "Veselin Kunchev", Url = new Uri("https://www.vkunchev.com/") },
                    License = new OpenApiLicense { Name = "---"},
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<AppDbContext>(options => {
                options.UseLazyLoadingProxies();
                options.UseInMemoryDatabase(Configuration.GetConnectionString("memory"));
            });

            services.AddScoped<IGatewayRepo, GatewayRepo>();
            services.AddScoped<IDeviceRepo, DeviceRepo>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IGatewayService, GatewayService>();

            services.AddAutoMapper(typeof(Startup));
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();

                app.UseSwagger().UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");
                    options.DocumentTitle = "Gateways API";
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
