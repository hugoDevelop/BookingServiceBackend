using BookingServiceBackend.Models;
using BookingServiceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservaController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservas()
        {
            var reservas = await _reservaService.ObtenerReservasAsync();
            return Ok(reservas);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReserva([FromBody] Reserva reserva)
        {
            await _reservaService.CrearReservaAsync(reserva);
            return CreatedAtAction(nameof(GetReservas), new { id = reserva.ReservaId }, reserva);
        }
    }
}
