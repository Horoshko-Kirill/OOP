using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public sealed record BookingReadDto(
    int Id,
    string Title,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    DateTime CreatedAt,
    int OrganizerId,
    int MeetingRoomId,
    UserDto Organizer,
    MeetingRoomDto MeetingRoom);
}
