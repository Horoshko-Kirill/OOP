using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MeetingRoom
    {
        public MeetingRoom(int id, string name, string description, int capacity, bool isActive, DateTime createAt, TimeSpan workStartTime,
            TimeSpan workEndTime)
        {
            Id = id;
            Name = name;
            Description = description;
            Capacity = capacity;
            IsActive = isActive;
            CreateAt = createAt;
            WorkStartTime = workStartTime;
            WorkEndTime = workEndTime;

            Bookings = new List<Booking>();
            Equipments = new List<Equipment>();
        }
        public int Id { get; set;    }
        public string Name { get; set; }
        public string Description {  get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        
        public TimeSpan WorkStartTime { get; set; }
        public TimeSpan WorkEndTime { get; set; }

        public DateTime CreateAt { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Equipment> Equipments { get; set; }

    }
}
