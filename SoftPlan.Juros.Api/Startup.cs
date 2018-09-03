using System.Collections.Generic;
using System.IO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Softplan.Api.AuthorizationServerPolicy;
using SoftPlan.Juros.Domain;
using SoftPlan.Juros.Domain.Service.Interface;
using Swashbuckle.AspNetCore.Swagger;

namespace SoftPlan.Juros.Api
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
            services.AddTransient<IJurosService, JurosService>();
            //services.AddTransient<IConfiguration>();
            ConfigureServicesAuthorizationServer(services);

            ConfigureServiceSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "API Juros SoftPlan");
                c.DocExpansion(DocExpansion.None);

            });
             app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                          name: "default",
                          template: "{controller=Swagger}/{action=Index}");
            });

            BindService();
        }

        private void BindService()
        {
            Kernel.StartKernel();
            Kernel.Bind<IJurosService, JurosService>();
        } 
        private void ConfigureServiceSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "API Juros SoftPlan",
                        Version = "v1",
                        Description = "",
                        Contact = new Contact
                        {
                            Name = "SoftPlan",
                            Url = "https://softplan.com.br"
                        }
                    });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);

                string caminhoAplicacao =
                        PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        private void ConfigureServicesAuthorizationServer(IServiceCollection services)
        {

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
           .AddIdentityServerAuthentication(x =>
           {
               x.Authority = Configuration.GetValue<string>("AuthorizationServer:Authority");
               x.ApiName = Configuration.GetValue<string>("AuthorizationServer:Scope");
               x.RequireHttpsMetadata = false;
           });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthorizationServer",
                    policy => policy.Requirements.Add(new AuthorizationServer()));
            });
        }
    }
}
