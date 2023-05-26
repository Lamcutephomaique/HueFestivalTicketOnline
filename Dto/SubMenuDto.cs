using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class SubMenuDto
    {
        [Key]
        public int SubMenuId { get; set; }
        public string Title { get; set; }
        public string PathIcon { get; set; }
        public int MenuId { get; set; }

    }
}
