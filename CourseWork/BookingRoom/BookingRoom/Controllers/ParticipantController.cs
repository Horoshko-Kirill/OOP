using Application.Services;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.Controllers
{

    [ApiController]
    [Route("api/participants")]
    public class ParticipantController : ControllerBase
    {

        private readonly IParticipantService _participantService;
        private readonly IMapper _mapper;

        public ParticipantController(IParticipantService participantService, IMapper mapper)
        {
            _participantService = participantService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipantReadDto>>> GetAll()
        {
            var participants = await _participantService.GetAllParticipant();
            return Ok(_mapper.Map<IEnumerable<ParticipantReadDto>>(participants));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantReadDto>> GetById(int id)
        {
            var participant = await _participantService.GetByIdParticipant(id);
            if (participant == null) return NotFound();
            return Ok(_mapper.Map<ParticipantReadDto>(participant));
        }

        [HttpGet("by-booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<ParticipantReadDto>>> GetByBooking(int bookingId)
        {
            var participants = await _participantService.GetByBooking(bookingId);
            return Ok(_mapper.Map<IEnumerable<ParticipantReadDto>>(participants));
        }

        [HttpGet("check-participation")]
        public async Task<ActionResult<bool>> IsUserParticipant(
            [FromQuery] int userId,
            [FromQuery] int bookingId)
        {
            var isParticipant = await _participantService.IsUserParticipant(userId, bookingId);
            return Ok(isParticipant);
        }

        [HttpPost]
        public async Task<ActionResult<ParticipantReadDto>> Create([FromBody] ParticipantWriteDto dto)
        {
            var participant = new Participant(
                id: 0,
                status: dto.Status,
                createdAt: DateTime.UtcNow,
                bookingId: dto.BookingId,
                userId: dto.UserId);

            await _participantService.CreateParticipant(participant);
            var createdParticipant = await _participantService.GetByIdParticipant(participant.Id);
            return CreatedAtAction(nameof(GetById), new { id = participant.Id }, _mapper.Map<ParticipantReadDto>(createdParticipant));
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] ParticipantWriteDto dto)
        {
            var participant = await _participantService.GetByIdParticipant(id);
            if (participant == null) return NotFound();

            participant.Status = dto.Status;
            await _participantService.UpdateParticipant(participant);

            var updatedParticipant = await _participantService.GetByIdParticipant(id);

            return Ok(_mapper.Map<ParticipantReadDto>(updatedParticipant));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var participant = await _participantService.GetByIdParticipant(id);
            if (participant == null) return NotFound();

            await _participantService.DeleteParticipant(participant);
            return NoContent();
        }
    }
}
