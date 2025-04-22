using WebBooking.Application;
using WebBooking.Domain;

namespace WebBookingTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            
            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));

            var start = DateTime.Today.AddHours(10);
            var end = DateTime.Today.AddHours(11);
            var capacity = 3;

            
            var availableRooms = roomService.GetAvailableRoom(start, end, capacity);
            var rooms = roomService.GetRooms();

            
            Assert.True(availableRooms.SequenceEqual(rooms)); 
        }

        [Fact]
        public void Test2()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
            roomService.AddRoom(3, "C", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var start = DateTime.Today.AddHours(10);
            var end = DateTime.Today.AddHours(11);
            var capacity = 3;


            var availableRooms = roomService.GetAvailableRoom(start, end, capacity);
            roomService.RemoveRoom(2);
            var rooms = roomService.GetRooms();


            Assert.True(availableRooms.SequenceEqual(rooms));
        }

        [Fact]
        public void Test3()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
            roomService.AddRoom(3, "C", 1, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var start = DateTime.Today.AddHours(10);
            var end = DateTime.Today.AddHours(11);
            var capacity = 3;


            var availableRooms = roomService.GetAvailableRoom(start, end, capacity);
            roomService.RemoveRoom(2);
            roomService.RemoveRoom(3);
            var rooms = roomService.GetRooms();


            Assert.True(availableRooms.SequenceEqual(rooms));
        }

        [Fact]
        public void Test4()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 3, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var organizer = new User
            {
                Id = 1,
                Email = "Admin@gmail.com",
                PasswordHash =  "1111",
                Role = UserRole.Admin,
                Bookings = new List<Booking>()
            };

            var participants = new List<User>
            {
                new User { 
                Id = 2,
                Email = "User1@gmail.com",
                PasswordHash =  "2222",
                Role = UserRole.Guest,
                Bookings = new List<Booking>() 
                },
                new User { 
                Id = 3,
                Email = "User2@gmail.com",
                PasswordHash =  "3333",
                Role = UserRole.Guest,
                Bookings = new List<Booking>() 
                }
            };

            var bookingService = new BookingService();

            var start = DateTime.Today.AddHours(10);
            var end = DateTime.Today.AddHours(11);
            var title = "Team";

            
            var booking = bookingService.CreateBooking(organizer, roomService.GetRooms()[0], start, end, title, participants);

            
            Assert.NotNull(booking);
            Assert.Equal(title, booking.Title);
            Assert.Equal(roomService.GetRooms()[0], booking.Room);
            Assert.Equal(organizer, booking.Organizer);
            Assert.Equal(start, booking.StartTime);
            Assert.Equal(end, booking.EndTime);

            Assert.True(booking.Participants.SequenceEqual(participants));

            Assert.Contains(booking, roomService.GetRooms()[0].Bookings);
            Assert.Contains(booking, organizer.Bookings);
            foreach (var user in participants)
                Assert.Contains(booking, user.Bookings);
        }


        [Fact]
        public void Test5()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 3, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var organizer = new User
            {
                Id = 1,
                Email = "Admin@gmail.com",
                PasswordHash = "1111",
                Role = UserRole.Admin,
                Bookings = new List<Booking>()
            };

            var participants = new List<User>
            {
                new User {
                Id = 2,
                Email = "User1@gmail.com",
                PasswordHash =  "2222",
                Role = UserRole.Guest,
                Bookings = new List<Booking>()
                },
                new User {
                Id = 3,
                Email = "User2@gmail.com",
                PasswordHash =  "3333",
                Role = UserRole.Guest,
                Bookings = new List<Booking>()
                }
            };

            var bookingService = new BookingService();

            var start = DateTime.Today.AddHours(10);
            var end = DateTime.Today.AddHours(11);
            var title = "Team";


            var booking = bookingService.CreateBooking(organizer, roomService.GetRooms()[0], start, end, title, participants);


            var exception = Assert.Throws<InvalidOperationException>(() =>
            bookingService.CreateBooking(organizer, roomService.GetRooms()[0], start, end, title, participants));

            Assert.Equal("Комната уже занята на выбранное время.", exception.Message);
        }


        [Fact]
        public void Test6()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 2, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var organizer = new User
            {
                Id = 1,
                Email = "Admin@gmail.com",
                PasswordHash = "1111",
                Role = UserRole.Admin,
                Bookings = new List<Booking>()
            };

            var participants = new List<User>
            {
                new User {
                Id = 2,
                Email = "User1@gmail.com",
                PasswordHash =  "2222",
                Role = UserRole.Guest,
                Bookings = new List<Booking>()
                },
                new User {
                Id = 3,
                Email = "User2@gmail.com",
                PasswordHash =  "3333",
                Role = UserRole.Guest,
                Bookings = new List<Booking>()
                }
            };

            var bookingService = new BookingService();

            var start = DateTime.Today.AddHours(10);
            var end = DateTime.Today.AddHours(11);
            var title = "Team";


            var exception = Assert.Throws<InvalidOperationException>(() =>
            bookingService.CreateBooking(organizer, roomService.GetRooms()[1], start, end, title, participants));

            Assert.Equal("Не хватает места в комнате.", exception.Message);
        }

        [Fact]
        public void Test7()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
            roomService.AddRoom(3, "C", 1, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var start = DateTime.Today.AddHours(10);
            var end = DateTime.Today.AddHours(11);


            var availableRooms = roomService.GetRoomAvailableTime(start, end);
            roomService.RemoveRoom(2);
            var rooms = roomService.GetRooms();


            Assert.True(availableRooms.SequenceEqual(rooms));
        }

        [Fact]
        public void Test8()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
            roomService.AddRoom(3, "C", 1, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var capacity = 3;


            var availableRooms = roomService.GetRoomAvailableCapacity(capacity);
            roomService.RemoveRoom(3);
            var rooms = roomService.GetRooms();


            Assert.True(availableRooms.SequenceEqual(rooms));
        }

        [Fact]
        public void Test9()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
            roomService.AddRoom(3, "C", 1, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var availableRooms = roomService.GetSortCapacityRoomsUp();

            List<Room> rooms = new List<Room>();

            rooms.Add(roomService.GetRooms()[2]);
            rooms.Add(roomService.GetRooms()[0]);
            rooms.Add(roomService.GetRooms()[1]);
           

            Assert.True(availableRooms.SequenceEqual(rooms));
        }

        [Fact]
        public void Test10()
        {

            var roomService = new RoomService();
            roomService.AddRoom(1, "A", 5, DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
            roomService.AddRoom(2, "B", 5, DateTime.Today.AddHours(10), DateTime.Today.AddHours(11));
            roomService.AddRoom(3, "C", 1, DateTime.Today.AddHours(9), DateTime.Today.AddHours(11));

            var availableRooms = roomService.GetSortTimeRoomsDown();

            List<Room> rooms = new List<Room>();

            rooms.Add(roomService.GetRooms()[1]);
            rooms.Add(roomService.GetRooms()[0]);
            rooms.Add(roomService.GetRooms()[2]);


            Assert.True(availableRooms.SequenceEqual(rooms));
        }
    }
}
