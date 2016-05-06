namespace MyFirstBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int postID { get; set; }

        public int? userID { get; set; }

        [StringLength(200)]
        public string postTitle { get; set; }

        [StringLength(500)]
        public string postSummary { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [Column(TypeName = "ntext")]
        public string postContent { get; set; }

        [StringLength(300)]
        public string friends { get; set; }

        public string imgThumbnail { get; set; }

        public string imageFolder { get; set; }
    }
}
