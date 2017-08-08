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
    
    public partial class InteractiveService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InteractiveService()
        {
            this.ContestantsReports_Day = new HashSet<ContestantsReports_Day>();
            this.ContestantsReports_Hour = new HashSet<ContestantsReports_Hour>();
            this.ContestantsReports_Month = new HashSet<ContestantsReports_Month>();
            this.ContestantsReports_Year = new HashSet<ContestantsReports_Year>();
            this.InteractiveServiceConfigs = new HashSet<InteractiveServiceConfig>();
            this.InteractiveServiceContestants = new HashSet<InteractiveServiceContestant>();
            this.InteractiveServiceContestantsVotes = new HashSet<InteractiveServiceContestantsVote>();
            this.InteractiveServiceUserMessages = new HashSet<InteractiveServiceUserMessage>();
        }
    
        public int ID { get; set; }
        public string InteractiveServiceName { get; set; }
        public string ServiceDesc { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string ImageURL { get; set; }
        public string ImageURL2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContestantsReports_Day> ContestantsReports_Day { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContestantsReports_Hour> ContestantsReports_Hour { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContestantsReports_Month> ContestantsReports_Month { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContestantsReports_Year> ContestantsReports_Year { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InteractiveServiceConfig> InteractiveServiceConfigs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InteractiveServiceContestant> InteractiveServiceContestants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InteractiveServiceContestantsVote> InteractiveServiceContestantsVotes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InteractiveServiceUserMessage> InteractiveServiceUserMessages { get; set; }
    }
}