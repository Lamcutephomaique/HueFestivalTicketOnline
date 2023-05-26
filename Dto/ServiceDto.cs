using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class ServiceDto
    {
        [Key]
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string PathImage { get; set; }
        public int TypeData { get; set; }
        public int SubMenuId { get; set; }
        public string Summary { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
        public string Content { get; set; }

    }
}
