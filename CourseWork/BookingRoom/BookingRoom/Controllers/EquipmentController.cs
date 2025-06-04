using Application.Services;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.Controllers
{

    [ApiController]
    [Route("api/equipment")]
    public class EquipmentController : ControllerBase
    {

        private readonly IEquipmentService _equipmentService;
        private readonly IMapper _mapper;

        public EquipmentController(IEquipmentService equipmentService, IMapper mapper)
        {
            _equipmentService = equipmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAll()
        {
            var equipment = await _equipmentService.GetAllEquipment();
            return Ok(_mapper.Map<IEnumerable<EquipmentDto>>(equipment));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDto>> GetById(int id)
        {
            var item = await _equipmentService.GetByIdEquipment(id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<EquipmentDto>(item));
        }

        [HttpGet("by-room/{roomId}")]
        public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetByMeetingRoom(int roomId)
        {
            var equipment = await _equipmentService.GetByMeetingRoom(roomId);
            return Ok(_mapper.Map<IEnumerable<EquipmentDto>>(equipment));
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentDto>> Create([FromBody] EquipmentDto dto)
        {
            var equipment = new Equipment(
                id: 0,
                name: dto.Name,
                description: dto.Description,
                meetingRoomId: dto.MeetingRoomId);

            await _equipmentService.CreateEquipment(equipment);
            return CreatedAtAction(nameof(GetById), new { id = equipment.Id }, _mapper.Map<EquipmentDto>(equipment));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipmentDto dto)
        {
            var equipment = await _equipmentService.GetByIdEquipment(id);
            if (equipment == null) return NotFound();

            equipment.Name = dto.Name;
            equipment.Description = dto.Description;
            equipment.MeetingRoomId = dto.MeetingRoomId;

            await _equipmentService.UpdateEquipment(equipment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipment = await _equipmentService.GetByIdEquipment(id);
            if (equipment == null) return NotFound();

            await _equipmentService.DeleteEquipment(equipment);
            return NoContent();
        }

    }
}
