﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            // dependency injection using iconfiguration service
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // add pool provides pooling, instead of creating a brand new instance, it checks to see if it has one
            // if there is one available, this is returned
            // better over dbcontext method
            // need to specify the provider and connection string
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));


            services.AddMvc().AddXmlSerializerFormatters();
            // if someone like the home controller requests the IEmployeeRepository service, create an instance of the MockeEmployeeRepository class
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            // addscoped because we want it to be alive and available for the entire scope of the request then a new instance upon a new request
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                //{
                //    SourceCodeLineCount = 10
                //};
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // placeholder {0} = status code
                // page not found will give a 302 to temp redirect to /error/404
                // 404 is passed because it was a 404 but the status code was never sent because it was sent a 302
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");

                // this re-executes the pipeline 
                // shows the status 404 and not a 302 redirect
                // the site traverses all the way down the pipeline, then on the way back it see's the 404
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            // file server over UseStaticFiles and UseDefaultFiles allows us to combine the functionality of the 2 and
            // add in UseDirectoryBrowser middleware
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");

            //app.UseFileServer();
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();

            //Conventional routing example:
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Some error processing the request");
            //    await context.Response.WriteAsync("Hosting Environment: " + env.EnvironmentName);
            //});
        }
    }
}
