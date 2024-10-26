using BookingServiceBackend.Data;
using BookingServiceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingServiceBackend.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BookingContext _context;

        public ClienteRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync(int companyId)
        {
            return await _context.Clientes.Where(c => c.CompanyId == companyId).ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int clienteId, int companyId)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == clienteId && c.CompanyId == companyId);
            return cliente ?? throw new Exception("Cliente no encontrado");
        }

        public async Task AddClienteAsync(Cliente cliente, int companyId)
        {
            // validar que no exista un cliente con el mismo email
            var clienteExistente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == cliente.Email && c.CompanyId == companyId);
            if (clienteExistente != null)
            {
                throw new Exception("Ya existe un cliente con el mismo email");
            }
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente, int companyId)
        {
            var clienteExistente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == cliente.Email && c.CompanyId == companyId);
            if (clienteExistente != null)
            {
                throw new Exception("Ya existe un cliente con el mismo email");
            }
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int clienteId, int companyId)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == clienteId && c.CompanyId == companyId) ?? throw new Exception("Cliente no encontrado");
            
            var reservas = await _context.Reservas.Where(r => r.ClienteId == clienteId).ToListAsync();
            if (reservas.Count > 0)
            {
                throw new Exception("No se puede eliminar el cliente porque tiene reservas asociadas");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
