using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace PYP_CustomIdentity.ViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(50)]
        public string Username { get; set; } = null!;
        [Required,MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required, MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required, MaxLength(50)]
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
