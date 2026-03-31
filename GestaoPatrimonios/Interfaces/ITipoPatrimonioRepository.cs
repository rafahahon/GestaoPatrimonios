using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface ITipoPatrimonioRepository
    {
        List<TipoPatrimonio> Listar();
        TipoPatrimonio BuscarPorId(Guid tipoPatrimonioId);
        TipoPatrimonio BuscarPorNome(string tipoPatrimonioId);
        void Adicionar(TipoPatrimonio tipoPatrimonio);
        void Atualizar(TipoPatrimonio tipoPatrimonio);
    }
}
