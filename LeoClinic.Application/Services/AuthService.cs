using LeoClinic.Application.DTOs;
using LeoClinic.Application.Interfaces;
using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;

namespace LeoClinic.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthService(
            IUserRepository userRepository,
            IVerificationCodeRepository verificationCodeRepository,
            IPasswordService passwordService,
            IJwtService jwtService,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _verificationCodeRepository = verificationCodeRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<string> Register(RegisterRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Password is required.");
            }

            if (await _userRepository.ExistsByEmailAsync(request.Email))
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var passwordHash = _passwordService.HashPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                Role = request.Role,
                IsBlocked = false,
                EmailConfirmed = false
            };

            if (request.Role == UserRole.Doctor)
            {
                if (request.Doctor == null)
                {
                    throw new ArgumentException("Doctor registration details are required for Doctor role.");
                }

                user.DoctorProfile = new DoctorProfile
                {
                    Price = request.Doctor.Price,
                    Bio = request.Doctor.Bio,
                    ContactNumber = request.Doctor.ContactNumber,
                    SpecialityId = request.Doctor.SpecialityId,
                    IsApproved = false
                };
            }
            else if (request.Role == UserRole.Patient)
            {
                user.PatientProfile = new PatientProfile
                {
                    ContactNumber = request.Patient?.ContactNumber ?? string.Empty,
                    DateOfBirth = request.Patient?.DateOfBirth ?? DateTime.MinValue,
                    Address = request.Patient?.Address ?? string.Empty,
                    IsApproved = true
                };
            }

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var code = await CreateVerificationCode(user.Id , VerificationType.EmailVerification);

            await _emailService.SendEmailAsync(
                user.Email,
                "LeoClinic - Email Verification Code",
                $"<h3>Welcome to LeoClinic, {user.FirstName}!</h3><p>Your email verification code is: <strong>{code}</strong></p><p>This code will expire in 15 minutes.</p>"
            );

            return "Registration successful. Please check your email for the verification code.";
        }

        public async Task<AuthResponseDTO> Login(LoginRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Email and password are required.");
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!user.EmailConfirmed)
            {
                throw new InvalidOperationException("Email is not verified. Please verify your email before logging in.");
            }

            if (user.IsBlocked)
            {
                throw new InvalidOperationException("Your account has been blocked.");
            }

            var (token, expiresAt) = _jwtService.GenerateToken(user);

            return new AuthResponseDTO
            {
                Token = token,
                ExpiresAt = expiresAt
            };
        }

        public async Task<string> VerifyEmail(VerifyEmailRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Code))
            {
                throw new ArgumentException("Email and verification code are required.");
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (user.EmailConfirmed)
            {
                return "Email is already verified.";
            }

            var verificationCode = await _verificationCodeRepository.GetLatestCodeAsync(user.Id, request.Code, VerificationType.EmailVerification);
            if (verificationCode == null)
            {
                throw new ArgumentException("Invalid verification code.");
            }

            if (verificationCode.UsedAt != null)
            {
                throw new InvalidOperationException("Verification code has already been used.");
            }

            if (verificationCode.ExpiresAt < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Verification code has expired.");
            }

            verificationCode.UsedAt = DateTime.UtcNow;
            user.EmailConfirmed = true;

            await _verificationCodeRepository.SaveChangesAsync();
            await _userRepository.SaveChangesAsync();

            return "Email verified successfully.";
        }

        public async Task<string> ResendVerificationCode(ResendCodeRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email is required.");
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (user.EmailConfirmed)
            {
                return "Email is already verified.";
            }

            var code = await CreateVerificationCode(user.Id, VerificationType.EmailVerification);

            await _emailService.SendEmailAsync(
                user.Email,
                "LeoClinic - New Verification Code",
                $"<h3>Hello {user.FirstName},</h3><p>Your new email verification code is: <strong>{code}</strong></p><p>This code will expire in 15 minutes.</p>"
            );

            return "A new verification code has been sent to your email.";
        }

        private async Task<string> CreateVerificationCode(int userId,VerificationType type)
        {
            var code = Random.Shared.Next(100000, 999999).ToString();
            var verificationCode = new VerificationCode
            {
                UserId = userId,
                Token = code,
                Type = VerificationType.EmailVerification,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                UsedAt = null
            };

            await _verificationCodeRepository.AddAsync(verificationCode);
            await _verificationCodeRepository.SaveChangesAsync();
            return code;
        }
    }
}
