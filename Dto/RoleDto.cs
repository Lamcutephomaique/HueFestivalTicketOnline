using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class RoleDto
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
