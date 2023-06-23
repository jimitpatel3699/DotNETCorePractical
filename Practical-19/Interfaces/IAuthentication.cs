using Microsoft.AspNetCore.Identity;
using Practical19.Models;

namespace Practical19.Interfaces
{
    public interface IAuthentication
    {
       Task<SignInResult> LoginAsync(LoginViewModel login);
       Task<IdentityResult> RegisterAsync(RegisterViewModel register);
    }
}
