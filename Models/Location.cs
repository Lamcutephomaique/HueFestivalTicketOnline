using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("Location")]
    public class Location
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

        public virtual ICollection<Event> Events { get; set; }
    }
}
