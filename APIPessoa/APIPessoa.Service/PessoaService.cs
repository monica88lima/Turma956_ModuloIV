using APIPessoa.Service.Dto;
using APIPessoa.Service.Entity;
using APIPessoa.Service.Interface;
using AutoMapper;

namespace APIPessoa.Service
{
    public class PessoaService : IPessoaService
    {
        private IPessoaRepository _repository;
        private IMapper _mapper;
        public PessoaService(IPessoaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PessoaDto> SelecionarPessoa(string nome)
        {
            PessoaEntity entity = await _repository.SelecionarPessoa(nome);
            PessoaDto pessoa = _mapper.Map<PessoaDto>(entity);
            return pessoa;
        }

        public async Task<List<PessoaDto>> SelecionarPessoas()
        {
            List<PessoaEntity> entity = await _repository.SelecionarPessoas();
            if (entity == null)
            {
                return null;
            }

            List<PessoaDto> pessoas = _mapper.Map<List<PessoaDto>>(entity);
            return pessoas;
        }

        public async Task<bool> AlterarPessoa(PessoaDto pessoa, int id)
        {
            PessoaEntity entity = _mapper.Map<PessoaEntity>(pessoa);
            return await _repository.AlterarPessoa(entity, id);
        }

        public async Task<bool> DeletarPessoa(int id)
        {
            return await _repository.DeletarPessoa(id);
        }

        public async Task<bool> InserirPessoa(PessoaDto pessoa)
        {
            PessoaEntity entity = _mapper.Map<PessoaEntity>(pessoa);
            return await _repository.InserirPessoa(entity);
        }
    }
}
