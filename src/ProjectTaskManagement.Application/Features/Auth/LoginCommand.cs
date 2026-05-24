using BCrypt.Net;
using MediatR;
using ProjectTaskManagement.Application.Dtos;
using ProjectTaskManagement.Application.Interfaces;

namespace ProjectTaskManagement.Application.Features.Auth
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;

    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email.Trim().ToLowerInvariant());
            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new InvalidOperationException("Email or password is incorrect.");
            }

            return new AuthResponse
            {
                Token = _jwtService.GenerateToken(user),
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
