using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using KocUniversityCourseManagement.Application.Interfaces;
using KocUniversityCourseManagement.Domain;
using KocUniversityCourseManagement.Domain.Interfaces;
using KocUniversityCourseManagement.Presentation.Models;
using Microsoft.Extensions.Configuration;
using static KocUniversityCourseManagement.Application.Services.UserService;

namespace KocUniversityCourseManagement.Application.Services
{
    public class KeycloakService : IKeycloakService
    {
        private readonly HttpClient _httpClient;
        private readonly string _keycloakAdminUrl;
        private readonly string _adminUsername;
        private readonly string _adminPassword;

        public KeycloakService(HttpClient httpClient, IConfiguration configuration)
        {
            //use appsettings.json 
            _httpClient = httpClient;
            _keycloakAdminUrl = "http://localhost:8080/auth";
            _adminUsername = "admin";
            _adminPassword = "admin";
        }

        public async Task<bool> RegisterUserAsync(UserRegisterModel model)
        {
            var token = await GetAdminTokenAsync();

            var user = new
            {
                username = model.Username,
                email = model.Email,
                enabled = true,
            };

            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync($"{_keycloakAdminUrl}/admin/realms/MyUniversityRealm/users", content);

            return response.IsSuccessStatusCode;
        }

        private async Task<string> GetAdminTokenAsync()
        {
            var tokenRequestParameters = new Dictionary<string, string>
                {
                    {"client_id", "admin-cli"},
                    {"username", _adminUsername}, 
                    {"password", _adminPassword}, 
                    {"grant_type", "password"}
                };

            var content = new FormUrlEncodedContent(tokenRequestParameters);
            var response = await _httpClient.PostAsync($"{_keycloakAdminUrl}/realms/master/protocol/openid-connect/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonSerializer.Deserialize<JsonElement>(responseString);
            var accessToken = tokenResponse.GetProperty("access_token").GetString();
            return accessToken;

        }
    }
}

