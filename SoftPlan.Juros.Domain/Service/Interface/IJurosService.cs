

namespace SoftPlan.Juros.Domain.Service.Interface
{
    public interface IJurosService
    {
        decimal CalcularJuros(double valorInicial, int meses);
        string Showmethecode();
    }
}
