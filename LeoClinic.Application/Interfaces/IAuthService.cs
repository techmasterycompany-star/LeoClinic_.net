using LeoClinic.Application.DTOs;

namespace LeoClinic.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDTO request);
        Task<AuthResponseDTO> Login(LoginRequestDTO request);
        Task<string> VerifyEmail(VerifyEmailRequestDTO request);
        Task<string> ResendVerificationCode(ResendCodeRequestDTO request);
    }
}
