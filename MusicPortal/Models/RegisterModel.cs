using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле нужно заполнить!")]
        [MaxLength(255)]
        [MinLength(2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле нужно заполнить!")]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле нужно заполнить!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле нужно заполнить!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Пароли не совпадают!")]
        public string? PasswordConfirmed { get; set; }
    }
}
