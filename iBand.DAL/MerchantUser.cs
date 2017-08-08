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
    
    public partial class MerchantUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MerchantUser()
        {
            this.MerchantOrders = new HashSet<MerchantOrder>();
            this.MerchantUserCategories = new HashSet<MerchantUserCategory>();
        }
    
        public int ID { get; set; }
        public Nullable<int> MyPaymentMerchantID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> Role { get; set; }
        public string PhoneNo { get; set; }
        public string EmailID { get; set; }
        public Nullable<System.DateTime> TranDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MerchantOrder> MerchantOrders { get; set; }
        public virtual Merchant Merchant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MerchantUserCategory> MerchantUserCategories { get; set; }
    }
}