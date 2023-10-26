using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class SeoMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان متا")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر مجاز250 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string MetaTitle { get; set; }
        [Display(Name = "کلمات کلید متا")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر مجاز350 می باشد")]
        public string MetaKeywords { get; set; }
        [Display(Name = "توضیحات متا")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر مجاز250 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string MetaDescription { get; set; }
        public int ParentId { get; set; }
        public string ParentTitle { get; set; }
        [Display(Name = "تگ ها")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر 350 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Tag { get; set; }
    }
}