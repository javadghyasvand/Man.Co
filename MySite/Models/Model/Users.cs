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
    
    [MetadataType(typeof(UsersMetadata))]
    public partial class Users
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public string UserImage { get; set; }
        public bool UserActive { get; set; }
    
        public virtual Roles Roles { get; set; }
    }
}
