//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpaceShoes.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OzellikDeger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OzellikDeger()
        {
            this.UrunOzellik = new HashSet<UrunOzellik>();
        }
    
        public int DegerId { get; set; }
        public string DegerAdi { get; set; }
        public int OzellikTipId { get; set; }
    
        public virtual OzellikTip OzellikTip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunOzellik> UrunOzellik { get; set; }
    }
}
