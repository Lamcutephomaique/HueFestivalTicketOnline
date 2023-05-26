using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class LocationDto
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string LocationImage { get; set; }
        public string Title { get; set; }
        public string LocationMap { get; set; }
        public string Summary { get; set; }
        public string Pathimage { get; set; }
        public string longtitude { get; set; }
        public string Lattitude { get; set; }
        public string Typedata { get; set; }
        public string Content { get; set; }
      
    }
}
