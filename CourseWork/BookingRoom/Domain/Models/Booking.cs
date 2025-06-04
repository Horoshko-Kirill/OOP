using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Booking
    {

        public Booking(int id, string title, string description, DateTime startTime, DateTime endTime, DateTime createdAt, int organizerId, int meetingRoomid) 
        {
            Id = id;
            Title = title;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            CreatedAt = createdAt;
            OrganizerId = organizerId;
            MeetingRoomid = meetingRoomid;
            Participants = new List<Participant>();
            Invitations = new List<Invitation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }


        
        public int OrganizerId { get; set; }

        
        public int MeetingRoomid { get; set; }

        public User Organizer { get; set;}

      
        public MeetingRoom MeetingRoom { get; set;}

   
        public ICollection<Participant> Participants { get; set; }

        public ICollection<Invitation> Invitations { get; set; }

    }
}
