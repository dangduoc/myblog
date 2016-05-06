namespace MyFirstBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BestMoment")]
    public partial class BestMoment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int momentID { get; set; }

        public int? userID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date_create { get; set; }

        public string Image { get; set; }

        [StringLength(300)]
        public string Summary { get; set; }

        [StringLength(100)]
        public string Title { get; set; }
    }
}
