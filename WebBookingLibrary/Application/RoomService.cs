using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebBooking.Domain;

namespace WebBooking.Application
{
    public class RoomService
    {

        private List<Room> rooms = new List<Room> ();

        public void AddRoom(int Id, string Name, int Capacity, DateTime StartWork, DateTime EndWork)
        {
            
            rooms.Add(new Room { Id = Id, Name = Name, Capacity = Capacity, StartWork = StartWork, EndWork = EndWork });

        }

        public void RemoveRoom(int Id)
        {
            foreach (var room in rooms)
            {
                if (room.Id == Id)
                {
                    rooms.Remove(room);
                    break;
                }
            }
        }


        public List<Room> GetRooms()
        {

            return new List<Room>(rooms);

        }

        public List<Room> GetSortTimeRoomsUp()
        {

            var sortedRooms = rooms.OrderBy(r => r.StartWork).ToList();

            return new List<Room>(sortedRooms);

        }

        public List<Room> GetSortTimeRoomsDown()
        {

            var sortedRooms = rooms.OrderByDescending(r => r.StartWork).ToList();

            return new List<Room>(sortedRooms);

        }

        public List<Room> GetSortCapacityRoomsUp()
        {

            var sortedRooms = rooms.OrderBy(r => r.Capacity).ToList();

            return new List<Room>(sortedRooms);

        }


        public List<Room> GetSortCapacityRoomsDown()
        {

            var sortedRooms = rooms.OrderByDescending(r => r.Capacity).ToList();

            return new List<Room>(sortedRooms);

        }

        public List<Room> GetAvailableRoom( DateTime start, DateTime end, int capacity)
        {
            
            List<Room> ans = new List<Room>();

            foreach (Room room in rooms)
            {

                if (room.IsAvailableTime(start, end) && room.IsAvailableCapacity(capacity))
                {
                    ans.Add(room);
                }

            }

            return new List<Room>(ans);


        }

        public List<Room> GetRoomAvailableTime(DateTime start, DateTime end)
        {

            List<Room> ans = new List<Room>();

            foreach (Room room in rooms)
            {

                if (start >= room.StartWork && end <= room.EndWork)
                {
                    ans.Add(room);
                }

            }

            return new List<Room>(ans);

        }

        public List<Room> GetRoomAvailableCapacity(int capacity)
        {

            List<Room> ans = new List<Room>();

            foreach (Room room in rooms)
            {

                if (room.IsAvailableCapacity(capacity))
                {
                    ans.Add(room);
                }

            }

            return new List<Room>(ans);

        }


    }
}
