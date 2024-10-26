using BookingServiceBackend.Models;
using BookingServiceBackend.Repositories;

namespace BookingServiceBackend.Services
{
    public class ClienteService
    {
        public readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync(int companyId) => await _clienteRepository.GetClientesAsync(companyId);
        public async Task<Cliente> GetClienteByIdAsync(int clienteId, int companyId) => await _clienteRepository.GetClienteByIdAsync(clienteId, companyId);
        public async Task AddClienteAsync(Cliente cliente, int companyId) => await _clienteRepository.AddClienteAsync(cliente, companyId);
        public async Task UpdateClienteAsync(Cliente cliente, int companyId) => await _clienteRepository.UpdateClienteAsync(cliente, companyId);
        public async Task DeleteClienteAsync(int clienteId, int companyId) => await _clienteRepository.DeleteClienteAsync(clienteId, companyId);
    }
}
