using LeoClinic.Application.DTOs;
using System.Threading.Tasks;

namespace LeoClinic.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDTO request);
        Task<AuthResponseDTO> Login(LoginRequestDTO request);
        Task<string> VerifyEmail(VerifyEmailRequestDTO request);
        Task<string> ResendVerificationCode(ResendCodeRequestDTO request);
        Task<AuthResponseDTO> RefreshToken(string refreshToken);
        Task Logout(string? refreshToken);
        Task<string> ForgotPassword(ForgotPasswordRequestDTO request);
        Task<string> VerifyResetCode(VerifyResetCodeRequestDTO request);
        Task<string> ResetPassword(ResetPasswordRequestDTO request);
    }
}
