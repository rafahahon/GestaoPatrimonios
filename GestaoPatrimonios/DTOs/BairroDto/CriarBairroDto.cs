namespace GestaoPatrimonios.DTOs.BairroDto
{
    public class CriarBairroDto
    {
        public string NomeBairro { get; set; } = string.Empty;
        public Guid CidadeID { get; set; }
    }
}
