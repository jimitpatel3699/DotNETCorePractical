using Microsoft.AspNetCore.Identity;

namespace Practical17.Models
{
    public class UserViewModel : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public IEnumerable<string>? Claims { get; set; }
    }

}
