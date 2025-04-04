﻿using BLL.DTO.UserDTOs;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task AddUserAsync(UserCreateDTO userDto);
        Task UpdateUserAsync(int id, UserUpdateDTO userDto);
        Task DeleteUserAsync(int id);
        Task<UserDTO> LoginAsync(string email, string password, string loginType);
        Task<UserDTO> RegisterAsync(UserCreateDTO userDto);
    }
}
