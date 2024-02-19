using Aws_Login.Application.InputModels;
using Aws_Login.Application.Ports;
using Aws_Login.Core.Entities;
using Aws_Login.Core.Ports;
using SecureIdentity.Password;

namespace Aws_Login.Application.Adapters
{
    public class UserService : IUserService
    {
        private readonly IUSerRepository _userRepository;

        public UserService(IUSerRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Create(CreateUserDto userDto)
        {
            var userEntity = (User)userDto;
            var password = userDto.Password;

            userEntity.PasswordHash = password;
            userEntity.PasswordHash = PasswordHasher.Hash(password);
            userEntity.Slug = userDto.Email.Replace("@", "-").Replace(".", "-");


            await _userRepository.CreateUser(userEntity);

            return userEntity.Email.ToString();
        }
    }
}
