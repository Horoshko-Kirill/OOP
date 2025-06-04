using Application.Services;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.Controllers
{
    [ApiController]
    [Route("api/meeting-rooms")]
    public class MeetingRoomController : ControllerBase
    {

        private readonly IMeetingRoomService _roomService;
        private readonly IMapper _mapper;

        public MeetingRoomController(IMeetingRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingRoomDto>>> GetAll()
        {
            var rooms = await _roomService.GetAllMeetingRoom();
            return Ok(_mapper.Map<IEnumerable<MeetingRoomDto>>(rooms));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingRoomDto>> GetById(int id)
        {
            var room = await _roomService.GetByIdMeetingRoom(id);
            if (room == null) return NotFound();
            return Ok(_mapper.Map<MeetingRoomDto>(room));
        }


        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<MeetingRoomDto>>> GetAvailable(
       [FromQuery] DateTime start,
       [FromQuery] DateTime end)
        {
            var rooms = await _roomService.GetAvailableRooms(start, end);
            return Ok(_mapper.Map<IEnumerable<MeetingRoomDto>>(rooms));
        }

        [HttpGet("by-capacity/{minCapacity}")]
        public async Task<ActionResult<IEnumerable<MeetingRoomDto>>> GetByCapacity(int minCapacity)
        {
            var rooms = await _roomService.GetRoomsByCapacity(minCapacity);
            return Ok(_mapper.Map<IEnumerable<MeetingRoomDto>>(rooms));
        }


        [HttpPost]
        public async Task<ActionResult<MeetingRoomDto>> Create([FromBody] MeetingRoomDto dto)
        {
            var room = new MeetingRoom(
                id: 0,
                name: dto.Name,
                description: dto.Description,
                capacity: dto.Capacity,
                isActive: true,
                createAt: DateTime.UtcNow,
                workStartTime: dto.WorkStartTime,
                workEndTime: dto.WorkEndTime);

            await _roomService.CreateMeetingRoom(room);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, _mapper.Map<MeetingRoomDto>(room));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MeetingRoomDto dto)
        {
            var room = await _roomService.GetByIdMeetingRoom(id);
            if (room == null) return NotFound();

            room.Name = dto.Name;
            room.Description = dto.Description;
            room.Capacity = dto.Capacity;
            room.IsActive = dto.IsActive;
            room.WorkStartTime = dto.WorkStartTime;
            room.WorkEndTime = dto.WorkEndTime;

            await _roomService.UpdateMeetingRoom(room);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomService.GetByIdMeetingRoom(id);
            if (room == null) return NotFound();

            await _roomService.DeleteMeetingRoom(room);
            return NoContent();
        }

    }
}
