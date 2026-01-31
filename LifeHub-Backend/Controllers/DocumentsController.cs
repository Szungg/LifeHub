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
    public class DocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DocumentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private string GetUserId()
        {
            return User.FindFirst("sub")?.Value ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            var userId = GetUserId();
            var documents = await _context.Documents
                .Where(d => d.UserId == userId)
                .OrderByDescending(d => d.UpdatedAt)
                .ToListAsync();

            return Ok(_mapper.Map<List<DocumentDto>>(documents));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var userId = GetUserId();
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            if (document == null)
                return NotFound();

            return Ok(_mapper.Map<DocumentDto>(document));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentDto dto)
        {
            var userId = GetUserId();

            var document = _mapper.Map<Document>(dto);
            document.UserId = userId;

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return Created($"api/documents/{document.Id}", _mapper.Map<DocumentDto>(document));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] UpdateDocumentDto dto)
        {
            var userId = GetUserId();
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            if (document == null)
                return NotFound();

            _mapper.Map(dto, document);
            document.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<DocumentDto>(document));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var userId = GetUserId();
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            if (document == null)
                return NotFound();

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
