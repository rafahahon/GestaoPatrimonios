using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public SolicitacaoTransferenciaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia.OrderByDescending(solicitacao => solicitacao.DataCriacaoSolicitacao).ToList();
        }

        public SolicitacaoTransferencia BuscarPorId(Guid transferenciaId)
        {
            return _context.SolicitacaoTransferencia.Find(transferenciaId);
        }

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public bool ExisteSolicitacaoPendente(Guid patrimonioId)
        {
            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                // se não encontrar o status "Pendente de aprovação", não há como existir uma solicitação pendente, então retorna false
                return false;
            }

            // se for true, retorna as informações da transferência pendente
            return _context.SolicitacaoTransferencia.Any(solicitacao =>
                solicitacao.PatrimonioID == patrimonioId &&
                solicitacao.StatusTransferenciaID == statusPendente.StatusTransferenciaID);
        }

        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId)
        {
            // se o id do usuario for realmente o id responsável pela localização informada, retorna true, caso contrário, retorna false
            return _context.Usuario.Any(usuario => usuario.UsuarioID == usuarioId &&
                usuario.Localizacao.Any(localizacao => localizacao.LocalizacaoID == localizacaoId));
        }

        public void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            _context.SolicitacaoTransferencia.Add(solicitacaoTransferencia);
            _context.SaveChanges();
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(localizacao => localizacao.LocalizacaoID == localizacaoId);
        }

        public Patrimonio BuscarPatrimonioPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }
    }
}
