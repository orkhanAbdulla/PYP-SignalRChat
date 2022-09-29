using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PYP_CustomIdentity.ViewModels
{
    public class LoginVM
    {
        [Required, MaxLength(50)]
        public string Username { get; set; } = null!;
        [Required, MaxLength (50)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
