using System.Linq.Expressions;
using Domain.Models;

namespace Application.Services
{
    public interface IEquipmentService
    {
        Task CreateEquipment(Equipment equipment);
        Task DeleteEquipment(Equipment equipment);
        Task<bool> ExistsEquipments(Expression<Func<Equipment, bool>> predicate);
        Task<IEnumerable<Equipment>> FindEquipments(Expression<Func<Equipment, bool>> predicate);
        Task<IEnumerable<Equipment>> GetAllEquipment();
        Task<Equipment> GetByIdEquipment(int id);
        Task<IEnumerable<Equipment>> GetByMeetingRoom(int roomId);
        Task UpdateEquipment(Equipment equipment);
    }
}