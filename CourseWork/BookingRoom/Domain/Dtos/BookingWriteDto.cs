using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public sealed record BookingWriteDto(
    int Id,
    string Title,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    DateTime CreatedAt,
    int OrganizerId,
    int MeetingRoomId);
}
