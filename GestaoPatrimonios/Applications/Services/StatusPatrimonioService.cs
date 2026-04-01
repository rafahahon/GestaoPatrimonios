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

        public void Adicionar(CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio? statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um status patrimônio cadastrado com esse nome.");
            }

            StatusPatrimonio status = new StatusPatrimonio
            {
                NomeStatus = dto.NomeStatus
            };

            _repository.Adicionar(status);
        }

        public void Atualizar(Guid statusId, CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio? statusBanco = _repository.BuscarPorId(statusId);

            if (statusBanco == null)
            {
                throw new DomainException("Tipo de status patrimônio não encontrado.");
            }

            StatusPatrimonio? statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um status patrimônio cadastrado com esse nome.");
            }

            statusBanco.NomeStatus = dto.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
}
