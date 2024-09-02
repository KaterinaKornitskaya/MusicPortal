using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class AuthorizationModel
    {
        [Display(Name ="Логін")]
        [Required(ErrorMessage = "Поле потрібно заповнити!")]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Поле потрібно заповнити!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Повторити пароль")]
        [Required(ErrorMessage = "Поле потрібно заповнити!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string? PasswordConfirmed { get; set; }
    }
}
