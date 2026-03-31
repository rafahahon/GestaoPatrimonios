using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.StatusPatrimonio;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;

        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusPatrimonioDto> Listar()
        {
            List<StatusPatrimonio> status = _repository.Listar();

            List<ListarStatusPatrimonioDto> statusDto = status.Select(status => new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = status.StatusPatrimonioID,
                NomeStatus = status.NomeStatus
            }).ToList();

            return statusDto;
        }

        public ListarStatusPatrimonioDto BuscarPorId(Guid statusId)
        {
            StatusPatrimonio? status = _repository.BuscarPorId(statusId);

            if (status == null)
            {
                throw new DomainException("Tipo de status patrimônio não encontrado.");
            }

            ListarStatusPatrimonioDto statusDto = new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = status.StatusPatrimonioID,
                NomeStatus = status.NomeStatus
            };

            return statusDto;
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
