namespace WDCApp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Service")]
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            Contract = new HashSet<Contract>();
            Development = new HashSet<Development>();
        }

        public int Id { get; set; }

        [Required]
        public string NameService { get; set; }

        public double? Price { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public int IdTypeOfService { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Development> Development { get; set; }

        public virtual TypeOfService TypeOfService { get; set; }
    }
}
