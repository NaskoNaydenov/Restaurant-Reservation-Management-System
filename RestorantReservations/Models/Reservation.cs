using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestorantReservations.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        [Required]
        public DateTime date { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage ="You have only 150 characters")]
        public string note { get; set; }

        [DisplayName("Table name")]
        [Required]
        public int? TableId { get; set; }
        public virtual Table? table { get; set; }
    }
}
