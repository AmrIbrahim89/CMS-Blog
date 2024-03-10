using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class Media
    {
        public Guid? ID { get; set; }
        public byte[]? Image {  get; set; } 
        public string? ImageSize { get; set; }
        public string? ImageName { get; set;}
    }
}
