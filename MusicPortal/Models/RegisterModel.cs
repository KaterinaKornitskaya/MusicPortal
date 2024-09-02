using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class RegisterModel
    {
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Поле потрібно заповнити!")]
        [MaxLength(255)]
        [MinLength(2)]
        public string? Name { get; set; }

        [Display(Name = "Логін")]
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
        [Compare("Password", ErrorMessage ="Пароли не совпадают!")]
        public string? PasswordConfirmed { get; set; }
    }
}
