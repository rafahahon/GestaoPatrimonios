using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public PatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Patrimonio> Listar()
        {
            return _context.Patrimonio.OrderBy(patrimonio => patrimonio.Denominacao).ToList();
        }

        public Patrimonio BuscarPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            var consulta = _context.Patrimonio.AsQueryable();

            if (patrimonioId.HasValue)
            {
                consulta = consulta.Where(p => p.PatrimonioID != patrimonioId.Value);
            }

            return consulta.FirstOrDefault(p => p.NumeroPatrimonio.ToLower() == numeroPatrimonio.ToLower());
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(localizacao => localizacao.LocalizacaoID == localizacaoId);
        }

        public bool StatusPatrimonioExiste(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Any(status => status.StatusPatrimonioID == statusPatrimonioId);
        }

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }
            _context.Patrimonio.Update(patrimonio);
            _context.SaveChanges();
        }

        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }
            _context.Patrimonio.Update(patrimonio);
            _context.SaveChanges();
        }
    }
}
