using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
  public class EventoPersist : IEventoPersist
  {
    private readonly ProEventosContext _context;
    public EventoPersist(ProEventosContext context)
    {
      _context = context;
    }

    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
      IQueryable<Evento> query = _context.Eventos.AsNoTracking()
        .Include(e => e.Lotes)
        .Include(e => e.RedesSociais);

      if (includePalestrantes)
      {
        query = query
        .Include(e => e.PalestrantesEventos)
        .ThenInclude(PE => PE.Palestrante);
      }

      query = query
        .OrderBy(e => e.Id);

      return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
    {
      IQueryable<Evento> query = _context.Eventos.AsNoTracking()
        .Include(e => e.Lotes)
        .Include(e => e.RedesSociais);

      if (includePalestrantes)
      {
        query = query
        .Include(e => e.PalestrantesEventos)
        .ThenInclude(PE => PE.Palestrante);
      }

      query = query
        .OrderBy(e => e.Id)
        .Where(e => e.Tema.ToLower().Contains(tema.ToLower())); //Filtrando com o ´tema´ recebido de parâmetro

      return await query.ToArrayAsync();
    }

    public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
    {
      IQueryable<Evento> query = _context.Eventos.AsNoTracking()
        .Include(e => e.Lotes)
         .Include(e => e.RedesSociais);

      if (includePalestrantes)
      {
        query = query
        .Include(e => e.PalestrantesEventos)
        .ThenInclude(PE => PE.Palestrante);
      }

      query = query
        .OrderBy(e => e.Id)
        .Where(e => e.Id == eventoId); //Filtrando com o ´EventoId´ recebido de parâmetro

      return await query.FirstOrDefaultAsync();
    }
  }
}