namespace WDCApp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contract")]
    public partial class Contract
    {
        public int Id { get; set; }

        public int? IdService { get; set; }

        [StringLength(50)]
        public string NumberContract { get; set; }

        public int? IdClient { get; set; }

        public string NameService { get; set; }

        public DateTime? Date { get; set; }

        public bool? Status { get; set; }

        public virtual Client Client { get; set; }

        public virtual Service Service { get; set; }
    }
}
