using Microsoft.Extensions.Configuration;
using SoftPlan.Juros.Domain;
using SoftPlan.Juros.Domain.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoftPlan.Juros.Test
{
    public class Start
    {
        public static IConfigurationRoot Configuration { get; set; }

        private static void Startconfig() {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            Kernel.GetKernel().Register<IConfiguration>(() => Configuration);
        }

        public static void BindServices()
        {
            Kernel.StartKernel();
            Startconfig();
            Kernel.Bind<IJurosService, JurosService>();
        }

    }
}
