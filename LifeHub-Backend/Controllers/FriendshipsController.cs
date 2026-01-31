using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LifeHub.Data;
using LifeHub.DTOs;
using LifeHub.Models;
using System.Security.Claims;

namespace LifeHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FriendshipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FriendshipsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private string GetUserId()
        {
            return User.FindFirst("sub")?.Value ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> GetFriendships()
        {
            var userId = GetUserId();
            var friendships = await _context.Friendships
                .Where(f => f.RequesterId == userId || f.ReceiverId == userId)
                .Include(f => f.Requester)
                .Include(f => f.Receiver)
                .ToListAsync();

            return Ok(_mapper.Map<List<FriendshipDto>>(friendships));
        }

        [HttpGet("accepted")]
        public async Task<IActionResult> GetAcceptedFriends()
        {
            var userId = GetUserId();
            var friends = await _context.Friendships
                .Where(f => (f.RequesterId == userId || f.ReceiverId == userId) 
                    && f.Status == FriendshipStatus.Accepted)
                .Include(f => f.Requester)
                .Include(f => f.Receiver)
                .ToListAsync();

            return Ok(_mapper.Map<List<FriendshipDto>>(friends));
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest([FromBody] CreateFriendshipDto dto)
        {
            var userId = GetUserId();

            if (userId == dto.ReceiverId)
                return BadRequest("No puedes enviarte una solicitud de amistad a ti mismo");

            var existingFriendship = await _context.Friendships
                .FirstOrDefaultAsync(f => 
                    (f.RequesterId == userId && f.ReceiverId == dto.ReceiverId) ||
                    (f.RequesterId == dto.ReceiverId && f.ReceiverId == userId));

            if (existingFriendship != null)
                return BadRequest("Ya existe una relación de amistad con este usuario");

            var friendship = new Friendship
            {
                RequesterId = userId,
                ReceiverId = dto.ReceiverId,
                Status = FriendshipStatus.Pending
            };

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return Created("", _mapper.Map<FriendshipDto>(friendship));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFriendship(int id, [FromBody] UpdateFriendshipDto dto)
        {
            var userId = GetUserId();
            var friendship = await _context.Friendships.FindAsync(id);

            if (friendship == null)
                return NotFound();

            if (friendship.ReceiverId != userId)
                return Forbid();

            friendship.Status = (FriendshipStatus)dto.Status;
            friendship.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<FriendshipDto>(friendship));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendship(int id)
        {
            var userId = GetUserId();
            var friendship = await _context.Friendships.FindAsync(id);

            if (friendship == null)
                return NotFound();

            if (friendship.RequesterId != userId && friendship.ReceiverId != userId)
                return Forbid();

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
