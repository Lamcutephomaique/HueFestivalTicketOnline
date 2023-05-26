using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("TicketLocation")]
    public class TicketLocation
    {
        [Key]
        public int TicketLocationId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        public string SDT { get; set; }
        [Required]
        public string Longtitude { get; set; }
        [Required]
        public string Laititude { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Arrange { get; set; }
        [Required]
        public int Md5 { get; set; }
        [Required]
        public DateTime Created_at { get; set; } = DateTime.Now;
        [Required]
        public DateTime Updated_at { get; set; } = DateTime.Now;


    }
}
