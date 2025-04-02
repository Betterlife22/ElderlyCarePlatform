using BLL.DTO.ServiceDTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ServiceService : IService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ServiceDTO>> GetAllServicesAsync()
        {
            var services = await _unitOfWork.GetRepository<Service>().GetAllAsync();
            return _mapper.Map<List<ServiceDTO>>(services);
        }

        public async Task<ServiceDTO?> GetServiceByIdAsync(int id)
        {
            var service = await _unitOfWork.GetRepository<Service>().GetByIdAsync(id);
            return service != null ? _mapper.Map<ServiceDTO>(service) : null;
        }

        public async Task AddServiceAsync(ServiceCreateDTO serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<Service>().InsertAsync(service);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateServiceAsync(int id, ServiceUpdateDTO serviceDto)
        {
            var repo = _unitOfWork.GetRepository<Service>();
            var service = await repo.GetByIdAsync(id);
            if (service == null) throw new Exception("Service not found");

            _mapper.Map(serviceDto, service); // Updates only relevant fields
            service.LastModified = DateTime.Now;

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(service);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteServiceAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Service>();
            var service = await repo.GetByIdAsync(id);
            if (service == null) throw new Exception("Service not found");

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
