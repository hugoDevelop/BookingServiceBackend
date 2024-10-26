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

        public async Task DeleteReservaAsync(int reservaId, int companyId)
        {
            try
            {
                var reserva = await _context.Reservas.Include(r => r.Servicio).FirstOrDefaultAsync(r => r.ReservaId == reservaId && r.Servicio.CompanyId == companyId) ?? throw new Exception("Reserva no encontrada");

                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reserva", ex);
            }
        }

        public async Task<Reserva> GetReservaByIdAsync(int reservaId)
        {
            try
            {
                var reserva = await _context.Reservas.FindAsync(reservaId);
                return reserva == null ? throw new Exception("Reserva no encontrada") : reserva;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la reserva", ex);
            }
        }

        public async Task<IEnumerable<Reserva>> GetReservasAsync(int companyId)
        {
            return await _context.Reservas.Include(r => r.Cliente).Include(r => r.Servicio).Where(r => r.Servicio.CompanyId == companyId && r.Cliente.CompanyId == companyId).ToListAsync();
        }

        public async Task UpdateReservaAsync(Reserva reserva, int companyId)
        {
            try
            {
                var reservaToUpdate = await _context.Reservas.Include(r => r.Servicio).FirstOrDefaultAsync(r => r.ReservaId == reserva.ReservaId && r.Servicio.CompanyId == companyId) ?? throw new Exception("Reserva no encontrada");

                reservaToUpdate.FechaReserva = reserva.FechaReserva;
                reservaToUpdate.ClienteId = reserva.ClienteId;
                reservaToUpdate.ServicioId = reserva.ServicioId;

                _context.Reservas.Update(reservaToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la reserva", ex);
            }
        }
    }
}
