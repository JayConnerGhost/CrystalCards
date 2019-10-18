﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CrystalCards.Api.helpers;
using CrystalCards.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
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
using Microsoft.IdentityModel.Tokens;
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("CardDatabase")));

            services.AddCors(options =>
              {
                  options.AddPolicy("CorsPolicy",
                      builder => builder.WithOrigins(
                              "http://ideas-web0.azurewebsites.net",
                              "http://localhost:4200",
                              "http://www.katiekatcoder.co.uk"
                              )
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
              });

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(options =>
                  {


                      options.TokenValidationParameters = new TokenValidationParameters()
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey =
                              new SymmetricSecurityKey(
                                  Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                          ValidateIssuer = false,
                          ValidateAudience = false

                      };
                  });

            services.AddSwaggerGen(c =>
              {
                  //The generated Swagger JSON file will have these properties.
                  c.SwaggerDoc("v1", new Info
                  {
                      Title = "Crystal Cards ",
                      Version = "v0.2",
                  });

                  c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                  {
                      Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                      Name = "Authorization",
                      In = "header",
                      Type = "apiKey"
                  });

                  //Locate the XML file being generated by ASP.NET...
                  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                  //... and tell Swagger to use those XML comments.
                  c.IncludeXmlComments(xmlPath);
              });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(joptions =>
            {
                joptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                joptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsEnvironment("Test"))
            {
                //test code in here 
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async ContextBoundObject =>
                        {
                            ContextBoundObject.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                            var error = ContextBoundObject.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                ContextBoundObject.Response.AddApplicationError(error.Error.Message);
                                await ContextBoundObject.Response.WriteAsync(error.Error.Message);
                            }
                        });
                });
            }

            // app.UseHttpsRedirection();
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

           app.UseSwaggerUI(s => {
                s.RoutePrefix = "help";
                s.SwaggerEndpoint("../swagger/v1/swagger.json", "Crystal Cards Documentation v0.2");
                s.InjectStylesheet("../css/swagger.min.css");
            });


           //This line enables the app to use Swagger, with the configuration in the ConfigureServices method.
           app.UseSwagger();
            app.UseMvc();


        }
    }
}
