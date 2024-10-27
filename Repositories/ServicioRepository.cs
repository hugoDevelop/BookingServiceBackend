using BookingServiceBackend.Data;
using BookingServiceBackend.Exceptions;
using BookingServiceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingServiceBackend.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly BookingContext _context;

        public ServicioRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task AddServicioAsync(Servicio servicio)
        {
            try
            {
                if (await _context.Companies.FirstOrDefaultAsync(c => c.Id == servicio.CompanyId) == null)
                    throw new NotFoundException("Compañía no encontrada");

                _context.Servicios.Add(servicio);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al agregar el servicio: " + ex.Message);
            }
        }

        public async Task DeleteServicioAsync(int servicioId, int companyId)
        {
            try
            {
                var servicio = await _context.Servicios.FirstOrDefaultAsync(s => s.ServicioId == servicioId && s.CompanyId == companyId) ?? throw new NotFoundException("Servicio no encontrado");
                
                if (await _context.Reservas.AnyAsync(r => r.ServicioId == servicioId && r.Servicio.CompanyId == companyId))
                {
                    throw new BadRequestException("No se puede eliminar el servicio porque tiene reservas asociadas");
                }
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al eliminar el servicio: " + ex.Message);
            }
        }

        public async Task<Servicio> GetServicioByIdAsync(int servicioId, int companyId)
        {
            try
            {
                var servicio = await _context.Servicios.FirstOrDefaultAsync(s => s.ServicioId == servicioId && s.CompanyId == companyId);
                return servicio ?? throw new NotFoundException("Servicio no encontrado");
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al obtener el servicio: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Servicio>> GetServiciosAsync(int companyId)
        {
            try
            {
                return await _context.Servicios.Where(s => s.CompanyId == companyId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al obtener los servicios: " + ex.Message);
            }
        }

        public async Task UpdateServicioAsync(Servicio servicio, int companyId)
        {
            try
            {
                var servicioToUpdate = await _context.Servicios.FirstOrDefaultAsync(s => s.ServicioId == servicio.ServicioId && s.CompanyId == companyId) ?? throw new NotFoundException("Servicio no encontrado");
                servicioToUpdate.Nombre = servicio.Nombre;
                servicioToUpdate.Precio = servicio.Precio;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al actualizar el servicio: " + ex.Message);
            }
        }
    }
}
