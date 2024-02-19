using Aws_Login.Application.InputModels;

namespace Aws_Login.Application.Ports;

public interface IUserService
{
    Task<string> Create(CreateUserDto userDto);

}
