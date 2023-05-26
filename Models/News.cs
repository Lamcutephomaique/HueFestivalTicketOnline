using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        public int NewsId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Video { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string Lang { get; set; }
        [Required]
        public string isnew { get; set; }
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longtitude { get; set; }
        [Required]
        public string arrange { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public DateTime Created_at { get; set; } = DateTime.Now;
        [Required]
        public DateTime Updated_at { get; set; } = DateTime.Now;
        [Required]
        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }

    }
}
