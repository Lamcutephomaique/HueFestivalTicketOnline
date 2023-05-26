using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("TicketType")]
    public class TicketType
    {
        [Key]
        public int TicketTypeId { get; set; }
        public string TicketName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}
