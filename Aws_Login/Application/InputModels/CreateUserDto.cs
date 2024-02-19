using Aws_Login.Core.Entities;

namespace Aws_Login.Application.InputModels
{
    public record CreateUserDto
    {
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
        public string? ConfirmPassword { get; init; }

        public static implicit operator User(CreateUserDto dto)
          => new()
          {
              Name = dto.Name,
              Email = dto.Email,
              PasswordHash = dto.Password

          };
    }
}
