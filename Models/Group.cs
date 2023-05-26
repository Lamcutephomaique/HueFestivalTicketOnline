using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicketOnline.Models
{
    [Table("Group")]
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }     
        public virtual ICollection<Event> Events { get; set; }
    }
}
