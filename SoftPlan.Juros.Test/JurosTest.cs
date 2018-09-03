using SoftPlan.Juros.Domain;
using SoftPlan.Juros.Domain.Service.Interface;
using System;
using Xunit;

namespace SoftPlan.Juros.Test
{
    public class JurosTest
    {
        IJurosService jurosService;
        public JurosTest()
        {
            Start.BindServices();
            jurosService = Kernel.Get<IJurosService>();
        }
        
        [Fact]
        public void CalcularJurosComposto()
        {
            decimal valorEsperado = 105.10m;
            Assert.Equal(jurosService.CalcularJuros(100, 5), valorEsperado);
        }
    }
}
