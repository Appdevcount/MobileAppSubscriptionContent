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
    
    public partial class CustomerWalletTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerWalletTransaction()
        {
            this.CustomerWalletTransactionsCredits = new HashSet<CustomerWalletTransactionsCredit>();
            this.CustomerWalletTransactionsDebits = new HashSet<CustomerWalletTransactionsDebit>();
        }
    
        public long ID { get; set; }
        public Nullable<long> CustomerWalletID { get; set; }
        public Nullable<int> ThirdPartyID { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Reference { get; set; }
        public string TransactionType { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual CustomerWallet CustomerWallet { get; set; }
        public virtual ThirdParty ThirdParty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerWalletTransactionsCredit> CustomerWalletTransactionsCredits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerWalletTransactionsDebit> CustomerWalletTransactionsDebits { get; set; }
    }
}