using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class TicketTypeDto
    {
        [Key]
        public int TicketTypeId { get; set; }
        public string TicketName { get; set; }
        public string Description { get; set; }

    }
}
