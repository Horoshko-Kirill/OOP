using System;
using System.Collections.Generic;
using WebBooking.Domain;
using WebBooking.Application;

namespace WebBooking.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
 
            var admin = new User { Id = 1, Email = "admin@gmail.com", PasswordHash = "admin123", Role = UserRole.Admin };
            var admin1 = new User { Id = 2, Email = "admin1@gmail.com", PasswordHash = "admin234", Role = UserRole.Admin };
            var admin2 = new User { Id = 3, Email = "admin2@gmail.com", PasswordHash = "admin345", Role = UserRole.Admin };
            var user1 = new User { Id = 4, Email = "user1@gmail.com", PasswordHash = "user123", Role = UserRole.Guest };
            var user2 = new User { Id = 5, Email = "user2@gmail.com", PasswordHash = "user234", Role = UserRole.Guest };
            var user3 = new User { Id = 6, Email = "user3@gmail.com", PasswordHash = "user345", Role = UserRole.Guest };
            var user4 = new User { Id = 7, Email = "user4@gmail.com", PasswordHash = "user456", Role = UserRole.Guest };
            var user5 = new User { Id = 8, Email = "user5@gmail.com", PasswordHash = "user567", Role = UserRole.Guest };

            var room1 = new Room
            {
                Id = 1,
                Name = "Room A",
                Capacity = 5,
                StartWork = DateTime.Today.AddHours(9),
                EndWork = DateTime.Today.AddHours(18)
            };

            var room2 = new Room
            {
                Id = 2,
                Name = "Room B",
                Capacity = 10,
                StartWork = DateTime.Today.AddHours(8),
                EndWork = DateTime.Today.AddHours(17)
            };

            var start = DateTime.Today.AddHours(10);
            var end = DateTime.Today.AddHours(11);
            var capacityNeeded = 3;

            var roomService = new RoomService();
            var bookingService = new BookingService();

   
            var availableRooms = roomService.GetAvailableRoom(rooms, start, end, capacityNeeded);

            Console.WriteLine("Доступные комнаты:");
            foreach (var room in availableRooms)
            {
                Console.WriteLine($"- {room.Name} (вместимость: {room.Capacity})");
            }

            if (availableRooms.Count == 0)
            {
                Console.WriteLine("Нет доступных комнат для бронирования.");
                return;
            }

            var selectedRoom = availableRooms[0];
            var participants = new List<User> { user1, user2 };

            try
            {
                var booking = bookingService.CreateBooking(admin, selectedRoom, start, end, "Team1", participants);
                Console.WriteLine($"\n Бронирование успешно создано: '{booking.Title}' в комнате {booking.Room.Name} с {booking.StartTime:t} до {booking.EndTime:t}");
                Console.WriteLine($"Организатор: {booking.Organizer.Email}");
                Console.WriteLine("Участники:");
                foreach (var p in booking.Participants)
                {
                    Console.WriteLine($"- {p.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Ошибка при создании бронирования: {ex.Message}");
            }

            availableRooms = roomService.GetAvailableRoom(rooms, start, end, capacityNeeded);

            Console.WriteLine("Доступные комнаты:");
            foreach (var room in availableRooms)
            {
                Console.WriteLine($"- {room.Name} (вместимость: {room.Capacity})");
            }

            if (availableRooms.Count == 0)
            {
                Console.WriteLine("Нет доступных комнат для бронирования.");
                return;
            }

            availableRooms = roomService.GetAvailableRoom(rooms, start, end, capacityNeeded);

            start = DateTime.Today.AddHours(15);
            end = DateTime.Today.AddHours(16);

            selectedRoom = availableRooms[0];

            participants = new List<User> { user1, user2 , user3, user4, user5};

            try
            {
                var booking = bookingService.CreateBooking(admin1, selectedRoom, start, end, "Team2", participants);
                Console.WriteLine($"\n Бронирование успешно создано: '{booking.Title}' в комнате {booking.Room.Name} с {booking.StartTime:t} до {booking.EndTime:t}");
                Console.WriteLine($"Организатор: {booking.Organizer.Email}");
                Console.WriteLine("Участники:");
                foreach (var p in booking.Participants)
                {
                    Console.WriteLine($"- {p.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Ошибка при создании бронирования: {ex.Message}");
            }

            foreach (var room in )
            {

            }

        }
    }
}
