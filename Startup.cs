using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using SurveySystem.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using SurveySystem.Models.Auth;
using SurveySystem.Migrations;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace SurveySystem
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<SurveyContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<UserDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SurveySecretKey_SurveySecretKey"));
            services.AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              }).AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = signingKey,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                }
            );
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.Use(async (context, next) =>
                {
                    await next();
                    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = $"{context.Request.Path.Value}/index.html";
                        await next();
                    }
                });
                app.UseHsts();
            }
            app.UseStaticFiles(); // For the wwwroot folder  
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Client")),
                RequestPath = "/Client"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Admin")),
                RequestPath = "/Admin"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets")),
                RequestPath = "/assets"
            });
            app.UseCors("Cors");
            //app.UseHttpsRedirection();
            app.UseMvc();
            //app.SeedData();
        }
    }
}
