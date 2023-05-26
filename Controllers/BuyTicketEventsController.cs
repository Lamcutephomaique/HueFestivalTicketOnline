using AutoMapper;
using HueFestivalTicketOnline.Dto;
using HueFestivalTicketOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;
using ZXing;

namespace HueFestivalTicketOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyTicketEventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly HueFestivalTicketOnlineContext _context;

        public BuyTicketEventsController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("/createqrcode")]
        public async Task<ActionResult<Ticket>> GenerateQRCode(TicketDto ticket)
        {
            BarcodeWriter writer = new BarcodeWriter();
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                Width = 100,
                Height = 100,
                DisableECI = true,
                CharacterSet = "UTF-8"
            };
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            var data = new
            {
                TicketName = ticket.Name,
                TicketTypeName = _context.TicketTypes.Where(x => x.TicketTypeId == ticket.TicketTypeId).Select(x => x.TicketName).FirstOrDefault(),
                ProgramName = _context.Events.Where(x => x.EventId == ticket.EventId).Select(x => x.EventName).FirstOrDefault(),
                UserName = _context.Users.Where(x => x.UserId == ticket.UserId).Select(x => x.UserName).FirstOrDefault(),
            }.ToString();

            Console.WriteLine(data);
            Bitmap qrCodeBitmap = writer.Write(data);

            MemoryStream ms = new MemoryStream();
            qrCodeBitmap.Save(ms, ImageFormat.Png);
            byte[] qrCodeBytes = ms.ToArray();

            string imagePath = "Image/" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".png";
            qrCodeBitmap.Save(imagePath, ImageFormat.Png);

            var ticketEntity = _mapper.Map<Ticket>(ticket);
            ticketEntity.Image = imagePath.ToString();
            _context.Tickets.Add(ticketEntity);
            await _context.SaveChangesAsync();
            return File(qrCodeBytes, "image/png");
        }

        [HttpPost("/scanqrcode")]
        public IActionResult DecodeQRCode(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var stream = file.OpenReadStream())
            {
                var reader = new BarcodeReader();
                var result = reader.Decode(new BitmapLuminanceSource(new Bitmap(stream)));

                if (result != null)
                {
                    string decodedData = result.Text;
                    return Ok(decodedData);
                }
                else
                {
                    return BadRequest("Unable to decode QR code.");
                }
            }
        }
    }
}
