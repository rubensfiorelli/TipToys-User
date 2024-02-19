using Aws_Login.Core.Entities;
using Aws_Login.Core.Ports;
using Aws_Login.Infrastruture.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Aws_Login.Infrastruture.Adapters
{
    public class UserRepository : IUSerRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task CreateUser(User user)
        {
            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException)
            {

                throw;
            }
        }

    }
}
