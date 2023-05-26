using System.ComponentModel.DataAnnotations;

namespace HueFestivalTicketOnline.Dto
{
    public class AdminDto
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public int? RoleId { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;
      
       
    }

}
