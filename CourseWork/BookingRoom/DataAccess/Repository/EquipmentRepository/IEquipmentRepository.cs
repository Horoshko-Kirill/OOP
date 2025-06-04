using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Models;

namespace DataAccess.Repository.EquipmentRepository
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {

        Task<IEnumerable<Equipment>> GetByMeetingRoomAsync(int roomId);

    }
}
