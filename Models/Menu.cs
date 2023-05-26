using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PathIcon { get; set; }
        [Required]
        public int TypeData { get; set; }

        public virtual ICollection<SubMenu> SubMenus { get; set; }

    }
}
