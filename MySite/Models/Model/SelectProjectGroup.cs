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
    
    [MetadataType(typeof(SelectProjectGroupMetadata))]
    public partial class SelectProjectGroup
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual ProjectGroups ProjectGroups { get; set; }
    }
}
