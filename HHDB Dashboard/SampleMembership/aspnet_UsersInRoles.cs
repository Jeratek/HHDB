//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SampleMembership
{
    using System;
    using System.Collections.Generic;
    
    public partial class aspnet_UsersInRoles
    {
        public System.Guid UserId { get; set; }
        public System.Guid RoleId { get; set; }
    
        public virtual aspnet_Roles aspnet_Roles { get; set; }
        public virtual aspnet_Roles aspnet_Roles1 { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual aspnet_Users aspnet_Users1 { get; set; }
    }
}