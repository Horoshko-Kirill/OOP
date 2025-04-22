using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBooking.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }

        public List<Booking> Bookings { get; set; } = new();

        public bool IsAvailableTime(DateTime start, DateTime end)
        {
            if (start < StartWork || end > EndWork)
            {
                return false;
            }

            for (int i = 0; i < Bookings.Count; i++)
            {
                if (!(start > Bookings[i].EndTime || end < Bookings[i].StartTime))
                {
                    return false;
                }
            }

            return true;
            
        }

        public bool IsAvailableCapacity(int capacity)
        {

            return capacity <= Capacity;

        }
    }

}
