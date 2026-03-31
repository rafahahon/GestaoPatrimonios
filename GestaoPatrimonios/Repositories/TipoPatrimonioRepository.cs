using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.OrderBy(tipoPatrimonio => tipoPatrimonio.NomeTipo).ToList();
        }

        public TipoPatrimonio BuscarPorId(Guid tipoPatrimonioId)
        {
            return _context.TipoPatrimonio.Find(tipoPatrimonioId);
        }

        public TipoPatrimonio BuscarPorNome(string nomeTipo)
        {
            return _context.TipoPatrimonio.FirstOrDefault(tipoPatrimonio => tipoPatrimonio.NomeTipo == nomeTipo);
        }

        public void Adicionar(TipoPatrimonio tipoPatrimonio)
        {
            _context.TipoPatrimonio.Add(tipoPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipoPatrimonio)
        {
            if (tipoPatrimonio == null)
            {
                return;
            }
            TipoPatrimonio tipoPatrimonioBanco = _context.TipoPatrimonio.Find(tipoPatrimonio.TipoPatrimonioID);

            if (tipoPatrimonioBanco == null)
            {
                return;
            }
            tipoPatrimonioBanco.NomeTipo = tipoPatrimonio.NomeTipo;
            _context.SaveChanges();
        }
    }
}
