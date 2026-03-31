using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface IStatusPatrimonioRepository
    {
        List<StatusPatrimonio> Listar();
        StatusPatrimonio BuscarPorId(Guid tipoStatusId);
        StatusPatrimonio BuscarPorNome(string nomeStatus);
        void Adicionar(StatusPatrimonio tipoStatus);
        void Atualizar(StatusPatrimonio tipoStatus);
    }
}
