using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("Checkin")]
    public class Checkin
    { 
        public int AdminId { get; set; }     
        public int TicketId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }       
        public virtual Ticket Ticket { get; set; }       
        public virtual Admin Admin { get; set; }
    }
}
