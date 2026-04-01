using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusTransferenciaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusTransferencia> Listar()
        {
            return _context.StatusTransferencia.OrderBy(nomeStatus => nomeStatus.NomeStatus).ToList();
        }

        public StatusTransferencia BuscarPorId(Guid statusTransferenciaId)
        {
            return _context.StatusTransferencia.Find(statusTransferenciaId);
        }

        public StatusTransferencia BuscarPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(tipoStatus => tipoStatus.NomeStatus == nomeStatus);
        } 

        public void Adicionar(StatusTransferencia statusTransferencia)
        {
            _context.StatusTransferencia.Add(statusTransferencia);
            _context.SaveChanges();
        }

        public void Atualizar(StatusTransferencia statusTransferencia)
        {
            if (statusTransferencia == null)
            {
                return;
            }
            StatusTransferencia statusTransferenciaBanco = _context.StatusTransferencia.Find(statusTransferencia.StatusTransferenciaID);

            if (statusTransferenciaBanco == null)
            {
                return;
            }
            statusTransferenciaBanco.NomeStatus = statusTransferencia.NomeStatus;
            _context.SaveChanges();
        }
    }
}
