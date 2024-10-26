using BookingServiceBackend.Models;
using BookingServiceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController(ServicioService servicioService) : ControllerBase
    {
        private readonly ServicioService _servicioService = servicioService;

        [HttpGet("v1/getServices")]
        public async Task<IActionResult> GetServicios()
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            var servicios = await _servicioService.GetServicesAsync(companyId ?? 0);
            return Ok(servicios);
        }

        [HttpPost("v1/saveService")]
        public async Task<IActionResult> CreateServicio([FromBody] Servicio servicio)
        {
            await _servicioService.AddServiceAsync(servicio);
            return CreatedAtAction(nameof(GetServicios), new { id = servicio.ServicioId }, servicio);
        }

        [HttpPut("v1/updateService/{id}")]
        public async Task<IActionResult> UpdateServicio(int id, [FromBody] Servicio servicio)
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            await _servicioService.UpdateServiceAsync(servicio, companyId ?? 0);
            return NoContent();
        }

        [HttpDelete("v1/deleteService/{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            await _servicioService.DeleteServiceAsync(id, companyId ?? 0);
            return NoContent();
        }
    }
}
