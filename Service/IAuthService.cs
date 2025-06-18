using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);
        Task<UserLoginResponseDTO?> LoginAsync(UserLoginDTO request);
        Task<UserLoginResponseDTO?> LoginAdmin(UserLoginDTO request);
    }
}
