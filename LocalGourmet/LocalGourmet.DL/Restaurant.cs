//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocalGourmet.DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Restaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurant()
        {
            this.Reviews = new HashSet<Review>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Cuisine { get; set; }
        public string Specialty { get; set; }
        public string Type { get; set; }
        public string Hours { get; set; }
        public string PhoneNumber { get; set; }
        public string WebAddress { get; set; }
        public bool Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
