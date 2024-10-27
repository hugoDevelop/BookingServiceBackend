using BookingServiceBackend.Data;
using BookingServiceBackend.Exceptions;
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
                throw new BadRequestException("Error al agregar la reserva: " + ex.Message);
            }
        }

        public async Task DeleteReservaAsync(int reservaId, int companyId)
        {
            try
            {
                var reserva = await _context.Reservas.Include(r => r.Servicio).FirstOrDefaultAsync(r => r.ReservaId == reservaId && r.Servicio.CompanyId == companyId) ?? throw new NotFoundException("Reserva no encontrada");

                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al eliminar la reserva: " + ex.Message);
            }
        }

        public async Task<Reserva> GetReservaByIdAsync(int reservaId)
        {
            try
            {
                var reserva = await _context.Reservas.FindAsync(reservaId);
                if (reserva == null) throw new NotFoundException("Reserva no encontrada");
                return reserva;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al obtener la reserva: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Reserva>> GetReservasAsync(int companyId)
        {
            try
            {
                return await _context.Reservas
                    .Where(r => r.Servicio.CompanyId == companyId && r.Cliente.CompanyId == companyId)
                    .Select(r => new Reserva
                    {
                        ReservaId = r.ReservaId,
                        FechaReserva = r.FechaReserva,
                        Cliente = new Cliente
                        {
                            ClienteId = r.Cliente.ClienteId,
                            Nombre = r.Cliente.Nombre,
                            Email = r.Cliente.Email
                        },
                        Servicio = new Servicio
                        {
                            ServicioId = r.Servicio.ServicioId,
                            Nombre = r.Servicio.Nombre,
                            Precio = r.Servicio.Precio
                        }
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al obtener las reservas: " + ex.Message);
            }
        }

        public async Task UpdateReservaAsync(Reserva reserva, int companyId)
        {
            try
            {
                var reservaToUpdate = await _context.Reservas.Include(r => r.Servicio).FirstOrDefaultAsync(r => r.ReservaId == reserva.ReservaId && r.Servicio.CompanyId == companyId) ?? throw new NotFoundException("Reserva no encontrada");

                reservaToUpdate.FechaReserva = reserva.FechaReserva;
                reservaToUpdate.ClienteId = reserva.ClienteId;
                reservaToUpdate.ServicioId = reserva.ServicioId;

                _context.Reservas.Update(reservaToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al actualizar la reserva: " + ex.Message);
            }
        }
    }
}
