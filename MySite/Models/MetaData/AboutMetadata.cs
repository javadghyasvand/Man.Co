using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MySite.Models
{
    public class AboutMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "لینک")]
        [MaxLength(450)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Link { get; set; }
        [Display(Name = "نام فایل")]
        [MaxLength(150)]
        public string FileName { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "عنوان")]
        [MaxLength(250)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string FileTitle { get; set; }

    }
}