using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Participant
    {

        public Participant(int id, ParticipantStatus status, DateTime createdAt, int bookingId, int userId)
        {
            Id = id;
            Status = status;
            CreatedAt = createdAt;
            BookingId = bookingId;
            UserId = userId;
        }

        public int Id { get; set; }

       
        public ParticipantStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BookingId { get; set; }
        public int UserId { get; set; }


        public Booking Booking { get; set; }
        public User User { get; set; }

    }
}
