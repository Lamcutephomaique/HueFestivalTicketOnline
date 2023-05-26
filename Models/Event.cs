using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("Event")]
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }

        public string EventName { get; set; }

        public string EventContent { get; set; }

        public string PathImage { get; set; }

        public int Price { get; set; }

        public int Type_inoff { get; set; }

        public int Type_program { get; set; }

        public virtual ProgramType ProgramType { get; set; }

        public int arrange { get; set; }

        public DateTime Fdate { get; set; } = DateTime.Now;

        public DateTime Tdate { get; set; } = DateTime.Now;

        public string Md5 { get; set; }

        public int Total { get; set; }

        /* public virtual ICollection<User> Users { get; set; }*/
        public virtual ICollection<FavouriteEvent> FavouriteEvents { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }





    }
}
