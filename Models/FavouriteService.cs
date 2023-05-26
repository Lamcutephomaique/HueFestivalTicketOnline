using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("FavouriteService")]
    public class FavouriteService
    {   
        public int UserId { get; set; }
     
        public int ServiceId { get; set; }
     
        public int Status { get; set; }
    
        public DateTime Created_at { get; set; } = DateTime.Now;
       
        public DateTime Updated_at { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
        public virtual Service Service { get; set; }

    }
}
