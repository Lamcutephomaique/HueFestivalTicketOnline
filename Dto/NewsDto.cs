using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class NewsDto
    {
        [Key]
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Video { get; set; }
        public string Url { get; set; }
        public string Comment { get; set; }
        public string Lang { get; set; }
        public string isnew { get; set; }
        public string latitude { get; set; }
        public string longtitude { get; set; }
        public string arrange { get; set; }
        public string Image { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;
        public int AdminId { get; set; }

    }
}
