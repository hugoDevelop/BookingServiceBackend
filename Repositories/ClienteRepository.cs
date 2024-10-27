using BookingServiceBackend.Data;
using BookingServiceBackend.Exceptions;
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
            try
            {
                return await _context.Clientes.Where(c => c.CompanyId == companyId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al obtener los clientes: " + ex.Message);
            }
        }

        public async Task<Cliente> GetClienteByIdAsync(int clienteId, int companyId)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == clienteId && c.CompanyId == companyId);
                return cliente ?? throw new NotFoundException("Cliente no encontrado");
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al obtener el cliente: " + ex.Message);
            }
        }

        public async Task AddClienteAsync(Cliente cliente, int companyId)
        {
            try
            {
                // validar que no exista un cliente con el mismo email
                var clienteExistente = await _context.Clientes.FirstOrDefaultAsync(c => (c.Email == cliente.Email || c.Nombre == cliente.Nombre) && c.CompanyId == companyId);
                if (clienteExistente != null)
                {
                    throw new BadRequestException("Ya existe un cliente con el mismo email o nombre");
                }
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al agregar el cliente: " + ex.Message);
            }
        }

        public async Task UpdateClienteAsync(Cliente cliente, int companyId)
        {
            try
            {
                var clienteExistente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId != cliente.ClienteId && (c.Email == cliente.Email || c.Nombre == cliente.Nombre) && c.CompanyId == companyId);
                if (clienteExistente != null)
                {
                    throw new BadRequestException("Ya existe un cliente con el mismo email o nombre");
                }
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al actualizar el cliente: " + ex.Message);
            }
        }

        public async Task DeleteClienteAsync(int clienteId, int companyId)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == clienteId && c.CompanyId == companyId) ?? throw new NotFoundException("Cliente no encontrado");

                var reservas = await _context.Reservas.Where(r => r.ClienteId == clienteId).ToListAsync();
                if (reservas.Count > 0)
                {
                    throw new BadRequestException("No se puede eliminar el cliente porque tiene reservas asociadas");
                }

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al eliminar el cliente: " + ex.Message);
            }
        }
    }
}

