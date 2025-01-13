using System.ComponentModel;

namespace RestorantReservations.Models
{
    public class Table
    {
        public int id { get; set; }
        public string Name { get; set; }
        [DisplayName("Capacity")]
        public int capacity { get; set; }
        [DisplayName("Available")]
        public bool available { get; set; } = true;

    }
}
