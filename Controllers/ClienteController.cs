using BookingServiceBackend.Models;
using BookingServiceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController(ClienteService clienteService) : ControllerBase
    {
        private readonly ClienteService _clienteService = clienteService;

        [HttpGet("v1/getClients")]
        public async Task<IActionResult> GetClientes()
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            var clientes = await _clienteService.GetClientesAsync(companyId ?? 0);
            return Ok(clientes);
        }

        [HttpPost("v1/saveClient")]
        public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            await _clienteService.AddClienteAsync(cliente, companyId ?? 0);
            return CreatedAtAction(nameof(GetClientes), new { id = cliente.ClienteId }, cliente);
        }

        [HttpPut("v1/updateClient/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            await _clienteService.UpdateClienteAsync(cliente, companyId ?? 0);
            return NoContent();
        }

        [HttpDelete("v1/deleteClient/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var companyId = HttpContext.Items["companyId"] as int?;
            await _clienteService.DeleteClienteAsync(id, companyId ?? 0);
            return NoContent();
        }
    }
}
