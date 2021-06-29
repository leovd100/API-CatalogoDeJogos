﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CatalogoDejogos.Middleware;
using ExemploApiCatalogoJogos.Controllers.V1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace CatalogoDejogos
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


           
            services.AddScoped<IJogoService, JogoService>();
            services.AddScoped<IJogoRepository, JogoRepository>();


            services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();
            services.AddScoped<IExemploSingleton, ExemploCicloDeVida>();
            services.AddTransient<IExemploSingleton, ExemploCicloDeVida>();



            services.AddSwaggerGen(c =>
            
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CatalogoDeJogos", Version = "v1" })

/*
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                c.IncludeXmlComments(Path.Combine(basePath, fileName));
                */
            );


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
                app.UseHsts();
            }

            //app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CatalogoDeJogos");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
