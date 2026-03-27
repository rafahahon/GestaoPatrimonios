using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.CidadeDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCidadeDto> Listar()
        {
            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDto> cidadesDto = cidades.Select(cidade => new ListarCidadeDto()
            {
                CidadeID = cidade.CidadeID,
                Estado = cidade.Estado
            }).ToList();

            return cidadesDto;
        }

        public ListarCidadeDto BuscarPorId(Guid cidadeId)
        {
            Cidade cidade = _repository.BuscarPorId(cidadeId);

            if (cidade == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            ListarCidadeDto cidadeDto = new ListarCidadeDto()
            {
                CidadeID = cidade.CidadeID,
                Estado = cidade.Estado
            };

            return cidadeDto;
        }
    }
}
