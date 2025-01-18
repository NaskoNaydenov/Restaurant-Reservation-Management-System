using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace RestorantReservations.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Date")]
        public DateTime date { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage ="You have only 150 characters")]
        [DisplayName("Description")]
        public string note { get; set; }

        [DisplayName("Table name")]
        [Required]
        public int? TableId { get; set; }
        [DisplayName("Table Name")]
        public virtual Table? table { get; set; }

        [Required]
        public string? UserId { get; set; }

        public virtual IdentityUser? User { get; set; }
    }
}