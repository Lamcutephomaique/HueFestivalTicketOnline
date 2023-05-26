using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("Notification")]
    public class Notification
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        public int EventName { get; set; }
        public DateTime FDate { get; set; }


    }
}
