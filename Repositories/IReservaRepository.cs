using BookingServiceBackend.Models;

namespace BookingServiceBackend.Repositories
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> GetReservasAsync();
        Task<Reserva> GetReservaByIdAsync(int reservaId);
        Task AddReservaAsync(Reserva reserva);
        Task UpdateReservaAsync(Reserva reserva);
        Task DeleteReservaAsync(int reservaId);
    }
}
