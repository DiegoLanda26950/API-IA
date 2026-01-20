using LibrosAPI.Models;
using LibrosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly LibroService _libroService;

        public LibrosController(LibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public IActionResult GetLibros()
        {
            return Ok(_libroService.GetLibros());
        }

        [HttpGet("{id}")]
        public IActionResult GetLibroById(int id)
        {
            var libro = _libroService.GetLibroById(id);
            return libro != null ? Ok(libro) : NotFound();
        }

        [HttpPost]
        public IActionResult AddLibro([FromBody] Libro libro)
        {
            var nuevoLibro = _libroService.AddLibro(libro);
            return CreatedAtAction(nameof(GetLibroById), new { id = nuevoLibro.Id }, nuevoLibro);
        }

        [HttpGet("autores-externos")]
        public async Task<IActionResult> GetAutoresExternos()
        {
            var autores = await _libroService.GetAutoresExternos();
            return Ok(autores);
        }
    }
}
