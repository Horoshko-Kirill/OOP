using System.Linq.Expressions;
using Domain.Models;

namespace Application.Services
{
    public interface IMeetingRoomService
    {
        Task CreateMeetingRoom(MeetingRoom meetingRoom);
        Task DeleteMeetingRoom(MeetingRoom meetingRoom);
        Task<bool> ExistsMeetingRooms(Expression<Func<MeetingRoom, bool>> predicate);
        Task<IEnumerable<MeetingRoom>> FindBookings(Expression<Func<MeetingRoom, bool>> predicate);
        Task<IEnumerable<MeetingRoom>> GetAllMeetingRoom();
        Task<IEnumerable<MeetingRoom>> GetAvailableRooms(DateTime start, DateTime end);
        Task<MeetingRoom> GetByIdMeetingRoom(int id);
        Task<IEnumerable<MeetingRoom>> GetRoomsByCapacity(int minCapacity);
        Task UpdateMeetingRoom(MeetingRoom meetingRoom);
    }
}