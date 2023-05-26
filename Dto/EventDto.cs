using System.ComponentModel.DataAnnotations;


namespace HueFestivalTicketOnline.Dto
{
    public class EventDto
    {
        [Key]
        public int EventId { get; set; }
        public int LocationId { get; set; }
        public int GroupId { get; set; }  
        public int AdminId { get; set; } 
        public string EventName { get; set; }
        public string EventContent { get; set; }
        public string PathImage { get; set; }
        public int Price { get; set; }
        public int Type_inoff { get; set; }
        public int Type_program { get; set; }
        public int arrange { get; set; }
        public DateTime Fdate { get; set; } = DateTime.Now;
        public DateTime Tdate { get; set; } = DateTime.Now;
        public string Md5 { get; set; }
        public int Total { get; set; }

    

    }
}
