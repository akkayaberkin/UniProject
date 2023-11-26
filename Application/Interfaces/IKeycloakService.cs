using System;
using KocUniversityCourseManagement.Domain;
using KocUniversityCourseManagement.Presentation.Models;

namespace KocUniversityCourseManagement.Application.Interfaces
{
	public interface IKeycloakService
    {
        Task<bool> RegisterUserAsync(UserRegisterModel model);
    }
}

