using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class GroupDto
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
  
    }
}
