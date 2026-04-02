using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.PatrimonioDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;

        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarPatrimonioDto> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            List<ListarPatrimonioDto> patrimoniosDto = patrimonios.Select(patrimonio => new ListarPatrimonioDto
            {
                PatrimonioID = patrimonio.PatrimonioID,
                Denominacao = patrimonio.Denominacao,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                LocalizacaoID = patrimonio.LocalizacaoID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            }).ToList();

            return patrimoniosDto;
        }

        public ListarPatrimonioDto BuscarPorId(Guid patrimonioId)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            return new ListarPatrimonioDto
            {
                PatrimonioID = patrimonio.PatrimonioID,
                Denominacao = patrimonio.Denominacao,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                LocalizacaoID = patrimonio.LocalizacaoID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            };
        }

        public void Adicionar(CriarPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.Denominacao);

            if (!_repository.LocalizacaoExiste(dto.LocalizacaoID))
            {
                throw new DomainException("Localização não encontrada.");
            }

            if (!_repository.TipoPatrimonioExiste(dto.TipoPatrimonioID))
            {
                throw new DomainException("Tipo patrimônio não encontrado.");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status patrimônio não encontrado.");
            }

            Patrimonio patrimonio = new Patrimonio
            {
                PatrimonioID = Guid.NewGuid(),
                Denominacao = dto.Denominacao,
                NumeroPatrimonio = dto.NumeroPatrimonio,
                LocalizacaoID = dto.LocalizacaoID,
                TipoPatrimonioID = dto.TipoPatrimonioID,
                StatusPatrimonioID = dto.StatusPatrimonioID
            };
            _repository.Adicionar(patrimonio);
        }

        public void Atualizar(Guid id, CriarPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.Denominacao);

            Patrimonio patrimonioBanco = _repository.BuscarPorId(id);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.LocalizacaoExiste(dto.LocalizacaoID))
            {
                throw new DomainException("Localização não encontrada.");
            }

            if (!_repository.TipoPatrimonioExiste(dto.TipoPatrimonioID))
            {
                throw new DomainException("Tipo patrimônio não encontrado.");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status patrimônio não encontrado.");
            }

            Patrimonio patrimonioExistente = _repository.BuscarPorNumeroPatrimonio(dto.NumeroPatrimonio);

            if (patrimonioExistente != null)
            {
                throw new DomainException("Já existe um patrimônio com esses dados.");
            }

            patrimonioBanco.Denominacao = dto.Denominacao;
            patrimonioBanco.NumeroPatrimonio = dto.NumeroPatrimonio;
            patrimonioBanco.LocalizacaoID = dto.LocalizacaoID;
            patrimonioBanco.TipoPatrimonioID = dto.TipoPatrimonioID;
            patrimonioBanco.StatusPatrimonioID = dto.StatusPatrimonioID;

            _repository.Atualizar(patrimonioBanco);
        }

        public void AtualizarStatus(Guid id, Guid statusPatrimonioId)
        {
            Patrimonio patrimonioBanco = _repository.BuscarPorId(id);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.StatusPatrimonioExiste(statusPatrimonioId))
            {
                throw new DomainException("Status patrimônio não encontrado.");
            }

            patrimonioBanco.StatusPatrimonioID = statusPatrimonioId;
            _repository.AtualizarStatus(patrimonioBanco);
        }
    }
}
