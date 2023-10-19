using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MySite.Models.ViewModel
{
    public class ProjectViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "عنوان نمونه کار")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر مجاز350 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string TitleProject { get; set; }
       
        [Display(Name = "لینک نمونه کار")]
        public string LinkProject { get; set; }

        [Display(Name = "تگ")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر مجاز350 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Tag { get; set; }

        [Display(Name = "تصویر اصلی نمونه کار")]
        [MaxLength(450, ErrorMessage = "تعداد کاراکتر مجاز450 می باشد")]
        public string ImageProject { get; set; }

        [Display(Name = "ویدیو نمونه کار")]
        [MaxLength(150)]
        public string VideoProject { get; set; }

        [Display(Name = "توضیحات نمونه کار")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string DescriptionProject { get; set; }

        [Display(Name = "عنوان متا")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر مجاز250 می باشد")]
        
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string MetaTitle { get; set; }

        [Display(Name = "تگ های متا")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر مجاز350 می باشد")]
        public string MetaKeywords { get; set; }

        [Display(Name = "توضیحات متا")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر مجاز250 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string MetaDescription { get; set; }
        public int IdSeo { get; set; }

    }
}