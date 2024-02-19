using Aws_Login.Core.Entities;

namespace Aws_Login.Application.InputModels
{
    public record LoginDto
    {
        public required string Email { get; init; }
        public required string Password { get; init; }

        public static implicit operator User(LoginDto dto)
           => new()
           {
               Email = dto.Email,
               PasswordHash = dto.Password

           };
    }
}
