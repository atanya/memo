using System.ComponentModel.DataAnnotations;

namespace SuperMemo.Models
{
    public class LoginInfoModel
    {
        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}