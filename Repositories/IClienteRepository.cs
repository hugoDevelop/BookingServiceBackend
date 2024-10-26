using BookingServiceBackend.Models;

namespace BookingServiceBackend.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientesAsync(int companyId);
        Task<Cliente> GetClienteByIdAsync(int clienteId, int companyId);
        Task AddClienteAsync(Cliente cliente, int companyId);
        Task UpdateClienteAsync(Cliente cliente, int companyId);
        Task DeleteClienteAsync(int clienteId, int companyId);
    }
}
