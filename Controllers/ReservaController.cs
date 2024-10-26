using BookingServiceBackend.Models;
using BookingServiceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController(ReservaService reservaService) : ControllerBase
    {
        private readonly ReservaService _reservaService = reservaService;

        [HttpGet("v1/getBookings")]
        public async Task<IActionResult> GetReservas()
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            var reservas = await _reservaService.ObtenerReservasAsync(companyId ?? 0);
            return Ok(reservas);
        }

        [HttpPost("v1/saveBooking")]
        public async Task<IActionResult> CreateReserva([FromBody] Reserva reserva)
        {
            await _reservaService.CrearReservaAsync(reserva);
            return CreatedAtAction(nameof(GetReservas), new { id = reserva.ReservaId }, reserva);
        }

        [HttpPut("v1/updateBooking/{id}")]
        public async Task<IActionResult> UpdateReserva(int id, [FromBody] Reserva reserva)
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            await _reservaService.ActualizarReservaAsync(reserva, companyId ?? 0);
            return NoContent();
        }

        [HttpDelete("v1/deleteBooking/{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            await _reservaService.EliminarReservaAsync(id, companyId ?? 0);
            return NoContent();
        }
    }
}
