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

        public async Task<IEnumerable<Reserva>> ObtenerReservasAsync(int companyId) => await _reservaRepository.GetReservasAsync(companyId);

        public async Task CrearReservaAsync(Reserva reserva) => await _reservaRepository.AddReservaAsync(reserva);

        public async Task ActualizarReservaAsync(Reserva reserva, int companyId) => await _reservaRepository.UpdateReservaAsync(reserva, companyId);

        public async Task EliminarReservaAsync(int reservaId, int companyId) => await _reservaRepository.DeleteReservaAsync(reservaId, companyId);

        public async Task<Reserva> ObtenerReservaPorIdAsync(int reservaId) => await _reservaRepository.GetReservaByIdAsync(reservaId);

    }
}
