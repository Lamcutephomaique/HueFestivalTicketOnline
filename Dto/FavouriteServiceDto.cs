using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{

    public class FavouriteServiceDto
    {   
        public int UserId { get; set; }
     
        public int ServiceId { get; set; }
     
        public int Status { get; set; }
    
        public DateTime Created_at { get; set; } = DateTime.Now;
       
        public DateTime Updated_at { get; set; } = DateTime.Now;
   
    }
}
