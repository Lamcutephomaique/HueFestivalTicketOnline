using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string PathImage { get; set; }
        public int TypeData { get; set; }
        public int SubMenuId { get; set; }
        public virtual ICollection<SubMenu> SubMenus { get; set; }
        public string Summary { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
        public string Content { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<FavouriteService> FavouriteServices { get; set; }

    }
}
