using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MySite.Models
{
    public class ProjectMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان نمونه کار")]
        [MaxLength(350)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string TitleProject { get; set; }
        [Display(Name = "لینک نمونه کار")]
        [MaxLength(450)]
        public string LinkProject { get; set; }
        [Display(Name = "تصویر اصلی نمونه کار")]
        [MaxLength(450)]
        public string ImageProject { get; set; }
        [Display(Name = "توضیحات نمونه کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string DescriptionProject { get; set; }
        [Display(Name = "ویدیو نمونه کار")]
        [MaxLength(150)]
        public string VideoProject { get; set; }
    }
}