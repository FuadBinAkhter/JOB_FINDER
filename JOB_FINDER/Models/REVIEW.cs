//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JOB_FINDER.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class REVIEW
    {
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public System.DateTime ReviewDate { get; set; }
        public string Description { get; set; }
    
        public virtual COMPANY COMPANY { get; set; }
        public virtual USER USER { get; set; }
    }
}
