using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MySite.Models.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string UserName { get; set; }

        [Display(Name = "کلمه ورود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool Remember { get; set; }
    }
}