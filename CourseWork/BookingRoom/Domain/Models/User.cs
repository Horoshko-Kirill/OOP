using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public User(int id, string name, string email, string password, UserRole role, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            CreatedAt = createdAt;
            OrganizedBookings = new List<Booking>();
            Participations = new List<Participant>();
            SentInvitations = new List<Invitation>();
            ReceivedInvitations = new List<Invitation>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }

       
        public ICollection<Booking> OrganizedBookings { get; set; }


        public ICollection<Participant> Participations { get; set; }


        public ICollection<Invitation> SentInvitations { get; set; }

  
        public ICollection<Invitation> ReceivedInvitations { get; set; } 

    }
}
