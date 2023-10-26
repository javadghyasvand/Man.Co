using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace MySite.Models
{
    public class UsersMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نقش کاربر")]
        public long RoleId { get; set; }
        [Display(Name = "نام کاربری ")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر مجاز350 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر مجاز350 می باشد")]
        [EmailAddress(ErrorMessage = "ایمیل شما نا معتبر می باشد")]
        [DataType(DataType.EmailAddress, ErrorMessage = "ایمیل شما نا معتبر می باشد")]
        public string UserEmail { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر مجاز50 می باشد")]
        [Phone(ErrorMessage = "شماره تماس راه به صورت صحیح وارد کنید")]
        [DataType(DataType.PhoneNumber)]
        public string UserPhone { get; set; }
        [Display(Name = "رمز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password, ErrorMessage = "شماره تماس راه به صورت صحیح وارد کنید")]
        public string UserPassword { get; set; }
        [Display(Name = "تصویر")]
        public string UserImage { get; set; }
      
    }
}