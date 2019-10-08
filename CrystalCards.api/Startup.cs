﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CrystalCards.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace CrystalCards.api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(joptions =>
            {
                joptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                joptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

              services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("CardDatabase")));

              services.AddCors(options =>
              {
                  options.AddPolicy("CorsPolicy",
                      builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
              });

              services.AddSwaggerGen(c =>
              {
                  //The generated Swagger JSON file will have these properties.
                  c.SwaggerDoc("v1", new Info
                  {
                      Title = "Crystal Cards ",
                      Version = "v0.2",
                  });

                  //Locate the XML file being generated by ASP.NET...
                  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                  //... and tell Swagger to use those XML comments.
                  c.IncludeXmlComments(xmlPath);
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            //Supporting file upload.
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Resources")),
                RequestPath=new PathString("/Resources")

            });


            //This line enables the app to use Swagger, with the configuration in the ConfigureServices method.
            app.UseSwagger();

            //This line enables Swagger UI, which provides us with a nice, simple UI with which we can view our API calls.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crystal Cards Documentation v0.2");
            });

        }
    }
}
