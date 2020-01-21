using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RestaurantData;
using RestaurantModel.Profiles;
using RestaurantWebAPI.Authentication;

namespace RestaurantWebAPI
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
            services.AddControllers(setupAction => 
            {
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));

                //setupAction.Filters.Add(
                //    new AuthorizeFilter());
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();

            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);


            services.AddDbContext<RestaurantDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RestaurantDBConnection"));
            });

            services.AddControllersWithViews()
                 .AddNewtonsoftJson();

            services.AddSwaggerGen(setupAction => 
            {
                setupAction.SwaggerDoc(
                    "RestaurantOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Restaurant API",
                        Version = "1",
                        Description = "Through this API you can access Restaurant, Restaurant Rating and CuisineType of Restaurant.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Name = "xyz",
                            Email = "xyz@gmail.com",
                            Url = new Uri("https://www.xyz.com")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                setupAction.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Input username and password to access this API."
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basicAuth"
                            }
                        }, new List<string>()
                    }
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
                setupAction.IncludeXmlComments(String.Format(Configuration["RestaurantModelXmlCommentFile"]));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(setupAction => 
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/RestaurantOpenApiSpecification/swagger.json",
                    "Restaurant API");
                setupAction.RoutePrefix = "";
            });
        }
    }
}
