using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface IEnderecoRepository
    {
        List<Endereco> Listar();
        Endereco BuscarPorId(Guid enderecoId);
        void Adicionar(Endereco endereco);
        void Atualizar(Endereco endereco);
        Bairro BuscarPorNomeEBairro(string nomeEndereco, Guid bairroId);
        bool BairroExiste(Guid bairroId);
    }
}
