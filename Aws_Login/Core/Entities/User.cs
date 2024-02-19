using System.Text.Json.Serialization;

namespace Aws_Login.Core.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? PasswordHash { get; set; }
        public string? Slug { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Role> Roles { get; set; } = [];

        public static readonly User NULL = new NullUser();
              
        private class NullUser : User { }
    }
}
