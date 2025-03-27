using BLL.DTO.ServiceDTOs;

namespace BLL.Interfaces
{
    public interface IService
    {
        Task<List<ServiceDTO>> GetAllServicesAsync();
        Task<ServiceDTO?> GetServiceByIdAsync(int id);
        Task AddServiceAsync(ServiceCreateDTO serviceDto);
        Task UpdateServiceAsync(int id, ServiceUpdateDTO serviceDto);
        Task DeleteServiceAsync(int id);
    }
}
