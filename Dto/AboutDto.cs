using System.ComponentModel.DataAnnotations;
namespace HueFestivalTicketOnline.Dto
{
 
    public class AboutDto
    {
        [Key]
        public int AboutId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; } = true;

    }
}
