﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ST.AppServicesLib;
using ST.SharedInterfacesLib;
using ST.SQLServerRepoLib;
using ST.Web.Data;
using ST.Web.Models;
using ST.Web.Services;

namespace ST.Web
{
    public class Startup
    {
        private readonly string _connectionStringAuth;
        private readonly string _connectionStringSupportTicket;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionStringSupportTicket = Environment.GetEnvironmentVariable(App.Constants.SupportTicketConnStringKey);
            _connectionStringAuth = Environment.GetEnvironmentVariable(App.Constants.SupportTicketConnStringKey);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureJson(services);

            ConfigureCors(services);

            ConfigureAppDI(services);

            ConfigureAuthentication(services);

            services.AddMvc();
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAnyOrigin",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
        }

        private static void ConfigureJson(IServiceCollection services)
        {
// Handle the case where it returns cyclically with an unclosed array! i.e. "[\\\ (unclosed"
            // See also the IgnoreJson attributes on the entities
            // https://stackoverflow.com/questions/48417166/rest-api-returns-bad-array-instead-of-json-object
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );
        }

        private static void ConfigureAppDI(IServiceCollection services)
        {
            services.AddTransient<ISTRepo, SQLRepo>();
            services.AddTransient<ISTAppService<ISTRepo>, STAppService<ISTRepo>>();
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(_connectionStringAuth ?? throw new InvalidOperationException(
                                      "No connection string for authentication database!")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            ConfigureIdentityOptions(services);

            ConfigureApplicationCookie(services);
        }

        private static void ConfigureApplicationCookie(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        private static void ConfigureIdentityOptions(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                ConfigurePassword(options);

                ConfigureLockout(options);

                ConfigureUserSettings(options);
            });
        }

        private static void ConfigureUserSettings(IdentityOptions options)
        {
            options.User.RequireUniqueEmail = true;
        }

        private static void ConfigureLockout(IdentityOptions options)
        {
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;
        }

        private static void ConfigurePassword(IdentityOptions options)
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 6;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddDebug();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            var logger = loggerFactory.CreateLogger("Default");
            
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/ST/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCors("AllowAnyOrigin");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ST}/{action=Index}/{id?}");
            });

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                appDbContext.Database.Migrate();
                scope.ServiceProvider.GetRequiredService<ISTRepo>().Initialise(_connectionStringSupportTicket);
            }
        }
    }
}
