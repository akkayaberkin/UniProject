using System;
using KocUniversityCourseManagement.Domain;

namespace KocUniversityCourseManagement.Application.Interfaces
{
	public interface IUserInterface
	{
        Task<ApplicationUser> GetUserByIdAsync(int id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task CreateUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(int id);
    }
}

