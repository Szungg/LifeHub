using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LifeHub.Infrastructure.Data;
using LifeHub.Api.DTOs;
using LifeHub.Domain.Entities.Users;
using LifeHub.Api.Utilidades;

namespace LifeHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MusicFilesController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MusicFilesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMusicFiles()
        {
            var authError = RequireAuthenticatedUserId(out var userId);
            if (authError != null)
                return authError;

            var files = await _context.MusicFiles
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return Ok(_mapper.Map<List<MusicFileDto>>(files));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMusicFile(int id)
        {
            var authError = RequireAuthenticatedUserId(out var userId);
            if (authError != null)
                return authError;

            var file = await _context.MusicFiles.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (file == null)
                return NotFoundError("Archivo de música no encontrado.");

            return Ok(_mapper.Map<MusicFileDto>(file));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMusicFile([FromBody] CreateMusicFileDto dto)
        {
            // ...existing code...
        }
    }
}
