//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iBand.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MerchantUserCategory
    {
        public int MetchantUserCategoryID { get; set; }
        public Nullable<int> MerchantUserID { get; set; }
        public Nullable<int> MerchantCategoryID { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
    
        public virtual MerchantCategory MerchantCategory { get; set; }
        public virtual MerchantUser MerchantUser { get; set; }
    }
}