using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;
using KalumManagement.Helpers;
using KalumManagement.Services;

namespace KalumManagement
{
    public class Startup
    {
        private readonly string OriginKalum = "OriginKalum";
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => { options.AddPolicy(name: OriginKalum, builder => {
                builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
            });
            });
            // services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddTransient<IQueueService,QueueAspiranteService>();
            services.AddControllers();
            services.AddDbContext<KalumDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddEndpointsApiExplorer();
            services.AddResponseCaching();
            services.AddControllers(options => options.Filters.Add(typeof(ErrorFilterException)));
            services.AddControllers(options => options.Filters.Add(typeof(ErrorFilterResponseException)));
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
                    = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(this.OriginKalum);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
            });
        }
    }
}