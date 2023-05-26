using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class MenuDto
    {
        [Key]
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string PathIcon { get; set; }
        public int TypeData { get; set; }      

    }
}
