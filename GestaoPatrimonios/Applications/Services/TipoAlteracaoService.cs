using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.TipoAlteracao;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class TipoAlteracaoService 
    {
        private readonly ITipoAlteracaoRepository _repository;

        public TipoAlteracaoService(ITipoAlteracaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoAlteracaoDto> Listar()
        {
            List<TipoAlteracao> tipos = _repository.Listar();

            List<ListarTipoAlteracaoDto> tiposDto = tipos.Select(tipo => new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipo.TipoAlteracaoID,
                NomeTipo = tipo.NomeTipo
            }).ToList();

            return tiposDto;
        }

        public ListarTipoAlteracaoDto BuscarPorId(Guid tipoId)
        {
            TipoAlteracao? tipo = _repository.BuscarPorId(tipoId);

            if (tipo == null)
            {
                throw new DomainException("Tipo de alteração não encontrado.");
            }

            ListarTipoAlteracaoDto tipoDto = new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipo.TipoAlteracaoID,
                NomeTipo = tipo.NomeTipo
            };

            return tipoDto;
        }

        public void Adicionar(CriarTipoAlteracaoDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoAlteracao? tipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Já existe um tipo de alteração cadastrado com esse nome.");
            }

            TipoAlteracao tipo = new TipoAlteracao
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid tipoId, CriarTipoAlteracaoDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoAlteracao? tipoBanco = _repository.BuscarPorId(tipoId);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo de alteração não encontrado.");
            }

            TipoAlteracao? tipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Já existe um tipo alteração cadastrado com esse nome.");
            }

            tipoBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
