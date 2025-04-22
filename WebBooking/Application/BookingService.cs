using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBooking.Domain;

namespace WebBooking.Application
{
    public class BookingService 
    {
        public Booking CreateBooking(User organizer, Room room, DateTime start, DateTime end, string title, List<User> participants)
        {
            if (!room.IsAvailableTime(start, end))
                throw new InvalidOperationException("Комната уже занята на выбранное время.");

            if (!room.IsAvailableCapacity(participants.Count + 1))
            {
                throw new InvalidOperationException("Не хватает места в комнате");
            }

            var booking = new Booking
            {
                Organizer = organizer,
                Room = room,
                Title = title,
                StartTime = start,
                EndTime = end,
                Participants = participants
            };

            room.Bookings.Add(booking);
            organizer.Bookings.Add(booking);
            foreach (var user in participants)
                user.Bookings.Add(booking);

            
            return booking;
        }
    }
}
