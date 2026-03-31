using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoAlteracaoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoAlteracao> Listar()
        {
            return _context.TipoAlteracao.OrderBy(tipoAlteracao => tipoAlteracao.NomeTipo).ToList();
        }

        public TipoAlteracao BuscarPorId(Guid tipoPatrimonioId)
        {
            return _context.TipoAlteracao.Find(tipoPatrimonioId);
        }

        public TipoAlteracao BuscarPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(tipoAlteracao => tipoAlteracao.NomeTipo == nomeTipo);
        }

        public void Adicionar(TipoAlteracao tipoAlteracao)
        {
            _context.TipoAlteracao.Add(tipoAlteracao);
            _context.SaveChanges();
        }

        public void Atualizar(TipoAlteracao tipoAlteracao)
        {
            if (tipoAlteracao == null)
            {
                return;
            }
            TipoAlteracao tipoAlteracaoBanco = _context.TipoAlteracao.Find(tipoAlteracao.TipoAlteracaoID);

            if (tipoAlteracaoBanco == null)
            {
                return;
            }
            tipoAlteracaoBanco.NomeTipo = tipoAlteracao.NomeTipo;
            _context.SaveChanges();
        }
    }
}
