using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.EquipmentRepository;
using DataAccess.UnitOfWork;
using Domain.Models;

namespace Application.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUnitOfWork _unitOfWork;


        public EquipmentService(IEquipmentRepository equipmentRepository, IUnitOfWork unitOfWork)
        {
            _equipmentRepository = equipmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateEquipment(Equipment equipment)
        {
            await _equipmentRepository.AddAsync(equipment);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEquipment(Equipment equipment)
        {
            await _equipmentRepository.DeleteAsync(equipment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistsEquipments(Expression<Func<Equipment, bool>> predicate)
        {
            return await _equipmentRepository.ExistsAsync(predicate);
        }

        public async Task<IEnumerable<Equipment>> FindEquipments(Expression<Func<Equipment, bool>> predicate)
        {
            return await _equipmentRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipment()
        {
            return await _equipmentRepository.GetAllAsync();
        }

        public async Task<Equipment> GetByIdEquipment(int id)
        {
            return await _equipmentRepository.GetByIdAsync(id);
        }

        public async Task UpdateEquipment(Equipment equipment)
        {
            await _equipmentRepository.UpdateAsync(equipment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Equipment>> GetByMeetingRoom(int roomId)
        {
            return await _equipmentRepository.GetByMeetingRoomAsync(roomId);
        }
    }
}
