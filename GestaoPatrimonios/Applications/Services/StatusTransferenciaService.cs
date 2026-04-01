using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.StatusTransferenciaDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class StatusTransferenciaService
    {
        private readonly IStatusTransferenciaRepository _repository;

        public StatusTransferenciaService(IStatusTransferenciaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusTransferenciaDto> Listar()
        {
            List<StatusTransferencia> statusTransferencias = _repository.Listar();

            List<ListarStatusTransferenciaDto> statusDto = statusTransferencias.Select(status => new ListarStatusTransferenciaDto()
            {
                StatusTransferenciaID = status.StatusTransferenciaID,
                NomeStatus = status.NomeStatus
            }).ToList();

            return statusDto;
        }

        public ListarStatusTransferenciaDto BuscarPorId(Guid statusId)
        {
            StatusTransferencia? status = _repository.BuscarPorId(statusId);

            if (status == null)
            {
                throw new DomainException("Tipo de status transferência não encontrado.");
            }

            ListarStatusTransferenciaDto statusDto = new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = status.StatusTransferenciaID,
                NomeStatus = status.NomeStatus
            };

            return statusDto;
        }

        public void Adicionar(CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusTransferencia? statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um status transferência cadastrado com esse nome.");
            }

            StatusTransferencia status = new StatusTransferencia
            {
                StatusTransferenciaID = Guid.NewGuid(),
                NomeStatus = dto.NomeStatus
            };

            _repository.Adicionar(status);
        }

        public void Atualizar(Guid statusId, CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusTransferencia? statusBanco = _repository.BuscarPorId(statusId);

            if (statusBanco == null)
            {
                throw new DomainException("Tipo de status transferência não encontrado.");
            }

            StatusTransferencia? statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um status transferência cadastrado com esse nome.");
            }

            statusBanco.NomeStatus = dto.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
}
