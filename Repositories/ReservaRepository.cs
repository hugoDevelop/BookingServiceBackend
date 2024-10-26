using BookingServiceBackend.Data;
using BookingServiceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingServiceBackend.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly BookingContext _context;

        public ReservaRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task AddReservaAsync(Reserva reserva)
        {
            try
            {
                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la reserva", ex);
            }
        }

        public Task DeleteReservaAsync(int reservaId)
        {
            throw new NotImplementedException();
        }

        public Task<Reserva> GetReservaByIdAsync(int reservaId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reserva>> GetReservasAsync()
        {
            return await _context.Reservas.Include(r => r.Cliente).Include(r => r.Servicio).ToListAsync();
        }

        public Task UpdateReservaAsync(Reserva reserva)
        {
            throw new NotImplementedException();
        }
    }
}
