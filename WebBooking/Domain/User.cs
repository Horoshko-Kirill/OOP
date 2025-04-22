using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBooking.Domain
{

    public enum UserRole { Guest, Admin }

    public class User
    {
        public int Id { get; set;  }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }

}
