using CsvHelper.Configuration;
using GestaoPatrimonios.DTOs.PatrimonioDto;

namespace GestaoPatrimonios.Applications.Mapeamentos
{
    // ClassMap -> é tipo um "tradutor de colunas", define como ler o csv, quais colunas ler, quais propriedades do objeto preencher, etc
    public class ImportarPatrimonioCsvMap : ClassMap<ImportarPatrimonioCsvDto>
    {
        // definindo os mapeamentos

        public ImportarPatrimonioCsvMap()
        {
            // map -> escolhe a propriedade da DTO
            // Name -> diz qual o nome da coluna no csv que deve ser lida para preencher a propriedade escolhida
            Map(m => m.NumeroPatrimonio).Name("N° invent.");
            Map(m => m.Denominacao).Name("Denominação do imobilizado");
            Map(m => m.DataIncorporacao).Name("Dt. incorp.");
            Map(m => m.ValorAquisicao).Name("ValAquis.");
        }
    }
}
