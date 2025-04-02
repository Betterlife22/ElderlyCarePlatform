using BLL.DTO.UserDTOs;
using System.Reflection.Metadata.Ecma335;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.GetRepository<User>().GetAllAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(id);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task AddUserAsync(UserCreateDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<User>().InsertAsync(user);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateUserAsync(int id, UserUpdateDTO userDto)
        {
            var repo = _unitOfWork.GetRepository<User>();
            var user = await repo.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            _mapper.Map(userDto, user); // Maps general user fields

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(user);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<User>();
            var user = await repo.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");

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

        public async Task<UserDTO> LoginAsync(string email, string password, string loginType)
        {
            var repo = _unitOfWork.GetRepository<User>();
            var user = (await repo.SearchAsync(u => u.UserName == email && u.Role == loginType)).FirstOrDefault() ?? throw new Exception("Email or password is incorrect!");
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new Exception("Email or password is incorrect!");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> RegisterAsync(UserCreateDTO userDto)
        {
            var repo = _unitOfWork.GetRepository<User>();
            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await repo.InsertAsync(user);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserDTO>(user);
        }
    }
}
