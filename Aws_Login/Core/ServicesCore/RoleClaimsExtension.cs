using Aws_Login.Core.Entities;
using System.Security.Claims;

namespace Aws_Login.Core.ServicesCore
{
    public static class RoleClaimsExtension
    {
        public static List<Claim> GetClaims(this User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email),
                new(ClaimTypes.Name, user.Id.ToString())

            };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));

            return claims;
        }

    }
}
