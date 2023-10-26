using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MySite.Models.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "کلمه عبور مغایرت دارد")]
        public string ReUserPassword { get; set; }

    }
}