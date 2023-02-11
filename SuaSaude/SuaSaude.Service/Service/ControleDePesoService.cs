using SuaSaude.Service.Dto;
using SuaSaude.Service.Entity;
using SuaSaude.Service.Interface;

namespace SuaSaude.Service.Service
{
    public class ControleDePesoService : IControleDePesoService
    {
        private IClassificacaoIMCRepository _classificacaoIMCRepository;
        
        public ControleDePesoService(IClassificacaoIMCRepository classificacaoIMCRepository)
        {
            _classificacaoIMCRepository = classificacaoIMCRepository;
        }

        public double CalcularIMC(double peso, double altura)
        {
            return peso / Math.Pow(altura, 2);
        }

        public string ClassificarIMC(double imc)
        {
            List<ClassificacaoIMCEntity> classificacoes = _classificacaoIMCRepository.ConsultaClassificacaoIMC();

            ClassificacaoIMCEntity? classificacao = classificacoes.FirstOrDefault(x =>
                                                                        imc > x.IMCInicial &&
                                                                        imc < x.IMCFinal);

            if (classificacao == null)
            {
                return "IMC não classificado";
            }

            return classificacao.Descricao;
        }

        public InformacoesIMCDto CategorizarIMC(double peso, double altura)
        {
            double imc = CalcularIMC(peso, altura);

            string classificacao = ClassificarIMC(imc);

            return new InformacoesIMCDto()
            {
                IMC = imc,
                Classificacao = classificacao
            };
        }


    }
}
