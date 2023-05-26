using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class NotificationDto
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int EventName { get; set; }
        public DateTime FDate { get; set; }

    }
}
