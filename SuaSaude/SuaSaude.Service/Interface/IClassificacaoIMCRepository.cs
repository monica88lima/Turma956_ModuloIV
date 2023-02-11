using SuaSaude.Service.Entity;

namespace SuaSaude.Service.Interface
{
    public interface IClassificacaoIMCRepository
    {
        List<ClassificacaoIMCEntity> ConsultaClassificacaoIMC();
    }
}
