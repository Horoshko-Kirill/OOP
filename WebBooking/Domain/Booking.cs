using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBooking.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Room Room { get; set; }
        public User Organizer { get; set; }
        public List<User> Participants { get; set; } = new();

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }

}
