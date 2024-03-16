using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Menu
    {
        [Key]
        public Guid MenuID { get; set; }

        [Required(ErrorMessage = "Menu must have a name.")]
        [StringLength(30)]
        public string MenuName { get; set; } = string.Empty;
        public List<Guid>? MenuElements { get; set; }
    }
}
