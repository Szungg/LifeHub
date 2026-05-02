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
    public class RecommendationsController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RecommendationsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecommendations()
        {
            var recommendations = await _context.Recommendations
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(_mapper.Map<List<RecommendationDto>>(recommendations));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecommendation(int id)
        {
            var recommendation = await _context.Recommendations
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recommendation == null)
                return NotFoundError("Recomendación no encontrada.");

            return Ok(_mapper.Map<RecommendationDto>(recommendation));
        }

        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserRecommendations(string userId)
        {
            var recommendations = await _context.Recommendations
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(_mapper.Map<List<RecommendationDto>>(recommendations));
        }
    }
}
