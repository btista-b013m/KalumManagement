using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
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
=======
using System.Text;
using System.Threading.Tasks;
using kalumAutManagement.DBContexts;
using kalumAutManagement.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace kalumAutManagement

{
    public class Startup
    {
        readonly string OriginKalum = "confKalumOrigin";
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration _Configuration)
        {
            this.Configuration = _Configuration;
>>>>>>> 8c782d89149ff6cca6be42a8f6889018b2794a80
        }

        public void ConfigureServices(IServiceCollection services)
        {
<<<<<<< HEAD
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
=======
           services.AddCors(options => { options.AddPolicy(name: OriginKalum, builder => {
                builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
            });
            });
            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey
                (
                  Encoding.UTF8.GetBytes(this.Configuration["JWT:key"])
                ),
                  ClockSkew = TimeSpan.Zero
              });
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AuthConnectionString"));

            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
>>>>>>> 8c782d89149ff6cca6be42a8f6889018b2794a80
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
<<<<<<< HEAD
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
=======
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(this.OriginKalum);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endPoins =>
            {
                endPoins.MapControllers();
            });

        }

    }

>>>>>>> 8c782d89149ff6cca6be42a8f6889018b2794a80
}