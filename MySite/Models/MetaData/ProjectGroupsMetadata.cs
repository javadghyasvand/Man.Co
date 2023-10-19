using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MySite.Models
{
    public class ProjectGroupsMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان گروه")]
        [MaxLength(250)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string GroupName { get; set; }
        [Display(Name = "تصویر گروه")]
        public string GroupImage { get; set; }
        [Display(Name = "توضیحات گروه" +
                        "")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string GroupDescription { get; set; }
    }
}