using Application.Requests.UserRequests;
using Application.Responses;

namespace Application.Services
{
    public interface IAuthService
    {
        Task<int> Register(RegistrationRequest request);
        Task<LoginResponse> Login(LoginRequest request);
    }
}
