using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface ITipoAlteracaoRepository
    {
        List<TipoAlteracao> Listar();
        TipoAlteracao BuscarPorId(Guid tipoAlteracaoId);
        TipoAlteracao BuscarPorNome(string nomeTipo);
        void Adicionar(TipoAlteracao tipoUsuario);
        void Atualizar(TipoAlteracao tipoUsuario);
    }
}
