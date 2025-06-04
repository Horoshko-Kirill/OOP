using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.MeetingRoomRepository
{
    public class MeetingRoomRepository : Repository<MeetingRoom>, IMeetingRoomRepository
    {

        public MeetingRoomRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<MeetingRoom>> GetAvailableRoomsAsync(DateTime start, DateTime end)
        {
            return await _dbSet
                .Where(room => room.IsActive)
                // Проверка, что запрашиваемое время в пределах рабочего времени комнаты
                .Where(room => start.TimeOfDay >= room.WorkStartTime &&
                              end.TimeOfDay <= room.WorkEndTime)
                // Проверка отсутствия пересечений с существующими бронированиями
                .Where(room => !room.Bookings.Any(b =>
                    (b.StartTime < end && b.EndTime > start)))
                .ToListAsync();
        }

        public async Task<IEnumerable<MeetingRoom>> GetRoomsByCapacityAsync(int minCapacity)
            => await _dbSet.Where(r => r.Capacity >= minCapacity).ToListAsync();

    }
}
