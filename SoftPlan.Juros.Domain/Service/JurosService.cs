using SoftPlan.Juros.Domain.Service.Interface;
using System;
using Microsoft.Extensions.Configuration;

namespace SoftPlan.Juros.Domain
{
    public class JurosService : IJurosService
    {
        IConfiguration configuration;
        public JurosService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public decimal CalcularJuros(double valorInicial, int meses)
        {
            return Convert.ToDecimal(String.Format("{0:N}", valorInicial * Math.Pow((1 + 0.01D), meses)));
        }

        public string Showmethecode()
        {
            return configuration.GetSection("UrlGitHub").Value;
        }
    }
}
