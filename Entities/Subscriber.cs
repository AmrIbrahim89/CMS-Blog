using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// This entity is used only to collect emails for subscribers, emails can be stored in some platforms (eg: mailchimp) 
    /// </summary>
    public class Subscriber
    {
        [Key]
        public Guid SubscriberID { get; set; }

        [Required(ErrorMessage = "Name is required to subscribe.")]
        [StringLength(20)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required to subscribe.")]
        [EmailAddress(ErrorMessage = "Email should be in correct format")]
        public string? Email { get; set; }
    }
}
