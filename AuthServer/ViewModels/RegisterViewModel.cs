using System.ComponentModel.DataAnnotations;

namespace AuthServer.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public string Team { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password doesn't Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
