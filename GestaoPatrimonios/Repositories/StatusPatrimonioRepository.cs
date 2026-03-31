using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.OrderBy(nomeStatus => nomeStatus.NomeStatus).ToList();
        }

        public StatusPatrimonio BuscarPorId(Guid statusId)
        {
            return _context.StatusPatrimonio.Find(statusId);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(tipoStatus => tipoStatus.NomeStatus == nomeStatus);
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio statusPatrimonio)
        {
            if (statusPatrimonio == null)
            {
                return;
            }
            StatusPatrimonio statusPatrimonioBanco = _context.StatusPatrimonio.Find(statusPatrimonio.StatusPatrimonioID);

            if (statusPatrimonioBanco == null)
            {
                return;
            }
            statusPatrimonioBanco.NomeStatus = statusPatrimonio.NomeStatus;
            _context.SaveChanges();
        }
    }
}
