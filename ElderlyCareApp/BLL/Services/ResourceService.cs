using BLL.DTO.ResourceDTOs;

namespace BLL.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResourceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResourceDTO>> GetAllResourcesAsync()
        {
            var resources = await _unitOfWork.GetRepository<Resource>().GetAllAsync();
            return _mapper.Map<List<ResourceDTO>>(resources);
        }

        public async Task<ResourceDTO?> GetResourceByIdAsync(int id)
        {
            var resource = await _unitOfWork.GetRepository<Resource>().GetByIdAsync(id);
            return resource != null ? _mapper.Map<ResourceDTO>(resource) : null;
        }

        public async Task AddResourceAsync(ResourceCreateDTO resourceDto)
        {
            var resource = _mapper.Map<Resource>(resourceDto);

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<Resource>().InsertAsync(resource);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateResourceAsync(int id, ResourceUpdateDTO resourceDto)
        {
            var repo = _unitOfWork.GetRepository<Resource>();
            var resource = await repo.GetByIdAsync(id);
            if (resource == null) throw new Exception("Resource not found");

            _mapper.Map(resourceDto, resource);

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(resource);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteResourceAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Resource>();
            var resource = await repo.GetByIdAsync(id);
            if (resource == null) throw new Exception("Resource not found");

            _unitOfWork.BeginTransaction();
            try
            {
                await repo.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}
