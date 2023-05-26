using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("SubMenu")]
    public class SubMenu
    {
        [Key]
        public int SubMenuId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PathIcon { get; set; }
        [Required]
        public int TypeData { get; set; }
        [Required]
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        public virtual ICollection<Service> Services { get; set; }


    }
}
