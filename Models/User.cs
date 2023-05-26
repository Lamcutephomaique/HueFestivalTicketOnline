using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CMND { get; set; }
        public string Avatar { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<FavouriteEvent> FavouriteEvents { get; set; }
        public virtual ICollection<FavouriteService> FavouriteServices { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Checkin> Checkins { get; set; }

    }
}
