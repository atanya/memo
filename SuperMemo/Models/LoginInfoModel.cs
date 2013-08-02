using System.ComponentModel.DataAnnotations;

namespace SuperMemo.Models
{
    public class LoginInfoModel
    {
        [Required(ErrorMessage = "Имя обязательно")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}