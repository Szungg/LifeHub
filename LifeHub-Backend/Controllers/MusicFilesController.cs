using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LifeHub.Data;
using LifeHub.DTOs;
using LifeHub.Models;

namespace LifeHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MusicFilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MusicFilesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private string GetUserId()
        {
            return User.FindFirst("sub")?.Value ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> GetMusicFiles()
        {
            var userId = GetUserId();
            var files = await _context.MusicFiles
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return Ok(_mapper.Map<List<MusicFileDto>>(files));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMusicFile(int id)
        {
            var userId = GetUserId();
            var file = await _context.MusicFiles.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (file == null)
                return NotFound();

            return Ok(_mapper.Map<MusicFileDto>(file));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMusicFile([FromBody] CreateMusicFileDto dto)
        {
            var userId = GetUserId();

            var musicFile = _mapper.Map<MusicFile>(dto);
            musicFile.UserId = userId;

            _context.MusicFiles.Add(musicFile);
            await _context.SaveChangesAsync();

            return Created($"api/musicfiles/{musicFile.Id}", _mapper.Map<MusicFileDto>(musicFile));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMusicFile(int id, [FromBody] UpdateMusicFileDto dto)
        {
            var userId = GetUserId();
            var musicFile = await _context.MusicFiles.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (musicFile == null)
                return NotFound();

            _mapper.Map(dto, musicFile);
            musicFile.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<MusicFileDto>(musicFile));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusicFile(int id)
        {
            var userId = GetUserId();
            var musicFile = await _context.MusicFiles.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (musicFile == null)
                return NotFound();

            _context.MusicFiles.Remove(musicFile);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
