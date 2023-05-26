using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("FavouriteEvent")]
    public class FavouriteEvent
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public virtual User User { get; set; }
        public virtual Event Event { get; set; }
    }
}
