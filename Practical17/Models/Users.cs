using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practical17.Models
{
    public class Users :IdentityUser
    {
        [Required]
        public string? Firstname { get; set; }

        [Required]
       
        public string? Lastname { get; set; }

    }
}
