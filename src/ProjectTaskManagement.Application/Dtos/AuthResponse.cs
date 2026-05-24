namespace ProjectTaskManagement.Application.Dtos
{
    public sealed class AuthResponse
    {
        public string Token { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}
