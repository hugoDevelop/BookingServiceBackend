using BookingServiceBackend.Models;

namespace BookingServiceBackend.Repositories
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> GetReservasAsync(int companyId);
        Task<Reserva> GetReservaByIdAsync(int reservaId);
        Task AddReservaAsync(Reserva reserva);
        Task UpdateReservaAsync(Reserva reserva, int companyId);
        Task DeleteReservaAsync(int reservaId, int companyId);
    }
}
