using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LifeHub.Api.DTOs;
using LifeHub.Domain.Entities.Users;
using LifeHub.Api.Utilidades;

namespace LifeHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFoundError("Usuario no encontrado.");

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var authError = RequireAuthenticatedUserId(out var userId);
            if (authError != null)
                return authError;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return UnauthorizedError("Sesión inválida. Inicia sesión de nuevo.");

            return Ok(await MapUserDtoAsync(user));
        }

        [HttpGet]
        [Authorize(Policy = "CanViewAdmin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserDto>();

            foreach (var user in users)
            {
                // ...existing code...
            }
            return Ok(result);
        }
    }
}
