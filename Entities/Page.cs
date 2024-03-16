using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Page
    {
        public Guid PageID { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Page Title Required.")]
        public string? Title { get; set; }

        [StringLength(50)]
        [RegularExpression("^[a-zA-Z-]+$", ErrorMessage = "Page slug should only contain letters and at least one hyphen (-).")]
        [Required(ErrorMessage = "Page slug cannot be empty.")]
        public string? PageSlug { get; set; }

        [Required(ErrorMessage = "Page Content Cannot Be Empty.")]
        public string? Content { get; set; }
        public byte[]? ImageData { get; set; }
        public string? PageUrl {  get; set; }
    }
}
