namespace MyFirstBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DailyPost")]
    public partial class DailyPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DailyPost()
        {
            imageDailyPosts = new HashSet<imageDailyPost>();
            imageDailyPosts1 = new HashSet<imageDailyPost>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int postID { get; set; }

        [Required]
        [StringLength(100)]
        public string postTitle { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string postContent { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateWrite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<imageDailyPost> imageDailyPosts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<imageDailyPost> imageDailyPosts1 { get; set; }
    }
}
