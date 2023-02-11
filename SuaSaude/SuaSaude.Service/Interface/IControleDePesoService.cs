using SuaSaude.Service.Dto;

namespace SuaSaude.Service.Interface
{
    public interface IControleDePesoService
    {
        double CalcularIMC(double peso, double altura);
        string ClassificarIMC(double imc);
        InformacoesIMCDto CategorizarIMC(double peso, double altura);
    }
}
