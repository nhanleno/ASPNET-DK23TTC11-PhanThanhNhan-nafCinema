using System.ComponentModel.DataAnnotations;

namespace nafCine.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public required string ConfirmPassword { get; set; }
    }
}