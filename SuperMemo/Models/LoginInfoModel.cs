using System.ComponentModel.DataAnnotations;

namespace SuperMemo.Models
{
    public class LoginInfoModel
    {
        [Required(ErrorMessage = "Имя обязательно")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(8, ErrorMessage = "Нужно хотя бы 8 символов")]
        public string Password { get; set; }
    }
}