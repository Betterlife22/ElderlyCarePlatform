using BLL.DTO.ResourceDTOs;

namespace BLL.Interfaces
{
    public interface IResourceService
    {
        Task<List<ResourceDTO>> GetAllResourcesAsync();
        Task<ResourceDTO?> GetResourceByIdAsync(int id);
        Task AddResourceAsync(ResourceCreateDTO resourceDto);
        Task UpdateResourceAsync(int id, ResourceUpdateDTO resourceDto);
        Task DeleteResourceAsync(int id);

    }
}
