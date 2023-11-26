using System;
using KocUniversityCourseManagement.Application.Interfaces;
using KocUniversityCourseManagement.Domain;
using KocUniversityCourseManagement.Domain.Interfaces;
using static KocUniversityCourseManagement.Application.Services.UserService;

namespace KocUniversityCourseManagement.Application.Services
{
        public class UserService : IUserInterface
    {
            private readonly IGenericRepository<ApplicationUser> _userRepository;

            public UserService(IGenericRepository<ApplicationUser> userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<ApplicationUser> GetUserByIdAsync(int id)
            {
                return await _userRepository.GetByIdAsync(id);
            }

            public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
            {
                return await _userRepository.GetAllAsync();
            }

            public async Task CreateUserAsync(ApplicationUser user)
            {
                await _userRepository.AddAsync(user);
            }

            public async Task UpdateUserAsync(ApplicationUser user)
            {
                await _userRepository.UpdateAsync(user);
            }

            public async Task DeleteUserAsync(int id)
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user != null)
                {
                    await _userRepository.DeleteAsync(user);
                }
        }
    }
}

