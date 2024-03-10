using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Page
    {
        public Guid PageID { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Page Title Required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Page Content Cannot Be Empty.")]
        public string? Content { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
