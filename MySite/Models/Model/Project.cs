//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MySite.Models.Model
{
    using System.ComponentModel.DataAnnotations;
    using  System.Collections.Generic;
    [MetadataType(typeof(ProjectMetadata))]
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            this.Gallery = new HashSet<Gallery>();
            this.SelectProjectGroup = new HashSet<SelectProjectGroup>();
            this.Seo = new HashSet<Seo>();
        }
    
        public int Id { get; set; }
        public string TitleProject { get; set; }
        public string LinkProject { get; set; }
        public string ImageProject { get; set; }
        public string DescriptionProject { get; set; }
        public string VideoProject { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gallery> Gallery { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectProjectGroup> SelectProjectGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seo> Seo { get; set; }
    }
}
