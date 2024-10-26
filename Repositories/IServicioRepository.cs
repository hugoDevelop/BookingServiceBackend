using BookingServiceBackend.Models;

namespace BookingServiceBackend.Repositories
{
    public interface IServicioRepository
    {
        Task<IEnumerable<Servicio>> GetServiciosAsync(int companyId);
        Task<Servicio> GetServicioByIdAsync(int servicioId, int companyId);
        Task AddServicioAsync(Servicio servicio);
        Task UpdateServicioAsync(Servicio servicio, int companyId);
        Task DeleteServicioAsync(int servicioId, int companyId);
    }
}
