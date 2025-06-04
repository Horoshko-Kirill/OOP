using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.MeetingRoomRepository;
using DataAccess.UnitOfWork;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MeetingRoomService : IMeetingRoomService
    {

        private readonly IMeetingRoomRepository _meetingRoomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MeetingRoomService(IMeetingRoomRepository meetingRoomRepository, IUnitOfWork unitOfWork)
        {
            _meetingRoomRepository = meetingRoomRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateMeetingRoom(MeetingRoom meetingRoom)
        {
            await _meetingRoomRepository.AddAsync(meetingRoom);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMeetingRoom(MeetingRoom meetingRoom)
        {
            await _meetingRoomRepository.DeleteAsync(meetingRoom);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistsMeetingRooms(Expression<Func<MeetingRoom, bool>> predicate)
        {
            return await _meetingRoomRepository.ExistsAsync(predicate);
        }

        public async Task<IEnumerable<MeetingRoom>> FindBookings(Expression<Func<MeetingRoom, bool>> predicate)
        {
            return await _meetingRoomRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<MeetingRoom>> GetAllMeetingRoom()
        {
            return await _meetingRoomRepository.GetAllAsync();
        }

        public async Task<MeetingRoom> GetByIdMeetingRoom(int id)
        {
            return await _meetingRoomRepository.GetByIdAsync(id);
        }

        public async Task UpdateMeetingRoom(MeetingRoom meetingRoom)
        {
            await _meetingRoomRepository.UpdateAsync(meetingRoom);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<MeetingRoom>> GetAvailableRooms(DateTime start, DateTime end)
        {
            return await _meetingRoomRepository.GetAvailableRoomsAsync(start, end);
        }

        public async Task<IEnumerable<MeetingRoom>> GetRoomsByCapacity(int minCapacity)
        {
            return await _meetingRoomRepository.GetRoomsByCapacityAsync(minCapacity);
        }

    }
}
