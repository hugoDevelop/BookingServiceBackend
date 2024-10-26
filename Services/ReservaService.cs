using BookingServiceBackend.Models;
using BookingServiceBackend.Repositories;

namespace BookingServiceBackend.Services
{
    public class ReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<IEnumerable<Reserva>> ObtenerReservasAsync() => await _reservaRepository.GetReservasAsync();

        public async Task CrearReservaAsync(Reserva reserva) => await _reservaRepository.AddReservaAsync(reserva);

    }
}
