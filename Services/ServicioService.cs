using BookingServiceBackend.Models;
using BookingServiceBackend.Repositories;

namespace BookingServiceBackend.Services
{
    public class ServicioService
    {
        private readonly IServicioRepository serviceRepository;

        public ServicioService(IServicioRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<Servicio>> GetServicesAsync(int companyId) => await serviceRepository.GetServiciosAsync(companyId);
        public async Task<Servicio> GetServiceByIdAsync(int serviceId, int companyId) => await serviceRepository.GetServicioByIdAsync(serviceId, companyId);
        public async Task AddServiceAsync(Servicio service) => await serviceRepository.AddServicioAsync(service);
        public async Task UpdateServiceAsync(Servicio service, int companyId) => await serviceRepository.UpdateServicioAsync(service, companyId);
        public async Task DeleteServiceAsync(int serviceId, int companyId) => await serviceRepository.DeleteServicioAsync(serviceId, companyId);
    }
}
