using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.EquipmentRepository
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {

        public EquipmentRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Equipment>> GetByMeetingRoomAsync(int roomId)
            => await _dbSet.Where(e => e.MeetingRoomId == roomId).ToListAsync();

    }
}
