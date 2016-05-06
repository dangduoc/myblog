namespace MyFirstBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userInfo")]
    public partial class userInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userID { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(10)]
        public string gender { get; set; }

        [StringLength(100)]
        public string addressLine { get; set; }

        [StringLength(50)]
        public string userImage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateCreate { get; set; }

        public virtual user user { get; set; }
    }
}
