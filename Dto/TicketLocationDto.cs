using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class TicketLocationDto
    {
        [Key]
        public int TicketLocationId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string SDT { get; set; }
        public string Longtitude { get; set; }
        public string Laititude { get; set; }
        public string Image { get; set; }
        public int Arrange { get; set; }
        public string Md5 { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;

    }
}
