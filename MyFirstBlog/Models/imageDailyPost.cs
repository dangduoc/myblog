namespace MyFirstBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("imageDailyPost")]
    public partial class imageDailyPost
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int postID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int imageNumber { get; set; }

        [StringLength(50)]
        public string imageName { get; set; }

        public virtual DailyPost DailyPost { get; set; }

        public virtual DailyPost DailyPost1 { get; set; }
    }
}
