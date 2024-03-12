using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using ExtentApplication_UserManagement.Components.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly ILogger<UserService> _logger;

    public UserService(HttpClient httpClient, NavigationManager navigationManager, ILogger<UserService> logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7008/");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _navigationManager = navigationManager;
        _logger = logger;
    }

    public async Task<bool> RegisterUserAsync(RegisterModel registerModel)
    {
        try
        {
            // Serialize the registerModel object to JSON
            var payload = JsonSerializer.Serialize(registerModel);

            // Create a StringContent instance from the serialized JSON payload
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync("api/register", content);
            if (response.IsSuccessStatusCode)
            {
                // Registration successful, redirect to login page
                _navigationManager.NavigateTo("/login");
                _logger.LogInformation("User registered successfully: {Username}, {Email}", registerModel.Username, registerModel.Email);
                return true;
            }
            else
            {
                // Registration failed
                _logger.LogError("User registration failed: Status code {StatusCode}, Reason {Reason}", response.StatusCode, response.ReasonPhrase);
                return false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            _logger.LogError(ex, "Error occurred while registering user: {Message}", ex.Message);
            return false;
        }
    }

    public async Task<bool> LoginAsync(LoginModel loginModel)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                // Login successful, redirect to dashboard or user's home page
                _navigationManager.NavigateTo("/dashboard");
                _logger.LogInformation("User logged in successfully: {Email}", loginModel.Email);
                return true;
            }
            else
            {
                // Login failed
                _logger.LogError("User login failed: Status code {StatusCode}, Reason {Reason}", response.StatusCode, response.ReasonPhrase);
                return false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            _logger.LogError(ex, "Error occurred while logging in user: {Message}", ex.Message);
            return false;
        }
    }

    public async Task<User> GetUserById(int id)
    {
        try
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"/users/{id}");
            if (user != null)
            {
                _logger.LogInformation("Retrieved user by ID: {UserId}", id);
            }
            else
            {
                _logger.LogWarning("User not found for ID: {UserId}", id);
            }
            return user;
        }
        catch (Exception ex)
        {
            // Handle exception
            _logger.LogError(ex, "Error occurred while retrieving user by ID: {UserId}", id);
            return null;
        }
    }

    public async Task<bool> UpdateUser(User user)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/users/{user.Id}", user);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("User updated successfully: {UserId}", user.Id);
                return true;
            }
            else
            {
                _logger.LogError("Failed to update user: Status code {StatusCode}, Reason {Reason}", response.StatusCode, response.ReasonPhrase);
                return false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            _logger.LogError(ex, "Error occurred while updating user: {Message}", ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("User deleted successfully: {UserId}", id);
                return true;
            }
            else
            {
                _logger.LogError("Failed to delete user: Status code {StatusCode}, Reason {Reason}", response.StatusCode, response.ReasonPhrase);
                return false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            _logger.LogError(ex, "Error occurred while deleting user: {Message}", ex.Message);
            return false;
        }
    }

    public async Task<bool> AddUser(User user)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", user);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("User added successfully: {UserId}", user.Id);
                return true;
            }
            else
            {
                _logger.LogError("Failed to add user: Status code {StatusCode}, Reason {Reason}", response.StatusCode, response.ReasonPhrase);
                return false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            _logger.LogError(ex, "Error occurred while adding user: {Message}", ex.Message);
            return false;
        }
    }
}
