using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MySite.Models
{
    public class ContactMetadata
    {
        [Key] public int Id { get; set; }

        [Display(Name = "نام")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر مجاز500 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر مجاز500 می باشد")]
        [DataType(DataType.EmailAddress, ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "شماره تماس")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر مجاز500 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.EmailAddress, ErrorMessage = "شماره وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }
        [Display(Name = "پیغام")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر مجاز850 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public System.DateTime MesageDate { get; set; }
        public bool IsCall { get; set; }
    }
}