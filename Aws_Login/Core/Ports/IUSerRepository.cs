using Aws_Login.Core.Entities;

namespace Aws_Login.Core.Ports
{
    public interface IUSerRepository
    {
        Task CreateUser(User user);
    }
}
