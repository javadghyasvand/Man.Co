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
    
    [MetadataType(typeof(GalleryMetadata))]
    public partial class Gallery
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImageTitle { get; set; }
        public int ProjectId { get; set; }
    
        public virtual Project Project { get; set; }
    }
}
