using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Equipment
    {

        public Equipment(int id, string name, string description, int meetingRoomId)
        {
            Id = id;
            Name = name;
            Description = description;
            MeetingRoomId = meetingRoomId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

       
        public int MeetingRoomId { get; set; }

        public MeetingRoom MeetingRoom { get; set; }

    }
}
