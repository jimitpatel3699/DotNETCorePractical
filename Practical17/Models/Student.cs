using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Practical17.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required,MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [ValidateNever] 
        public int Age { get; set; }
        [Required]
        public string Address {get; set; }
    }
}
