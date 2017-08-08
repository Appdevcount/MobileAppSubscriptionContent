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
    
    public partial class Tone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tone()
        {
            this.RBTs = new HashSet<RBT>();
        }
    
        public int ID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> AlbumID { get; set; }
        public Nullable<int> ArtistID { get; set; }
        public string ToneName { get; set; }
        public string ToneDescription { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string ImageURL { get; set; }
    
        public virtual Album Album { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Category Category { get; set; }
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RBT> RBTs { get; set; }
    }
}