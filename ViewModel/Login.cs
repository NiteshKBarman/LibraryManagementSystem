using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModel
{
    public class Login
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a Gmail")]
        [Display(Name = "Email")]
        public string? Gmail { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [Display(Name = "Passowrd")]
        public string? Passowrd { get; set; }
    }
}
