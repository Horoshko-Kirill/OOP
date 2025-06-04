using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Invitation
    {
        public Invitation(int id, InvitationStatus status, DateTime sentAt, DateTime? respondedAt, int bookingId, int invitedUserId, int inviterId)
        {
            Id = id;
            Status = status;
            SentAt = sentAt;
            RespondedAt = respondedAt;
            BookingId = bookingId;
            InvitedUserId = invitedUserId;
            InviterId = inviterId;
        }

        public int Id { get; set; }
        public InvitationStatus Status { get; set; }

        public DateTime SentAt { get; set; }

        
        public DateTime? RespondedAt { get; set; }


        public int BookingId { get; set; }

        
        public int InvitedUserId { get; set; }

      
        public int InviterId { get; set; }

        
        public Booking Booking { get; set; }

     
        public User InvitedUser { get; set; }

      
        public User Inviter { get; set; }
    }
}
