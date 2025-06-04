using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Models;

namespace DataAccess.Repository.MeetingRoomRepository
{
    public interface IMeetingRoomRepository : IRepository<MeetingRoom>
    {

        Task<IEnumerable<MeetingRoom>> GetAvailableRoomsAsync(DateTime start, DateTime end);
        Task<IEnumerable<MeetingRoom>> GetRoomsByCapacityAsync(int minCapacity);

    }
}
