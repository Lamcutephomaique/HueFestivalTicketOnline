using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class FavouriteEventDto
    {
        [Key]
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
      
    }
}
