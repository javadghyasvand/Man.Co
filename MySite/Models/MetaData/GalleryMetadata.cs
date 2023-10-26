using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class GalleryMetadata
    {
        [Key] 
        public int Id { get; set; }
        public string ImageName { get; set; }
        [Display(Name = "عنوان عکس")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر مجاز250 می باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string ImageTitle { get; set; }
        public int ProjectId { get; set; }
    }
}