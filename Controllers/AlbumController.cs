using LibrosAPI.Models;
using LibrosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumController(AlbumService albumService)
        {
            _albumService = albumService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            return Ok(await _albumService.GetAlbums());
        }

        // GET FILTRADO
        [HttpGet("filtrar")]
        public async Task<IActionResult> FiltrarAlbums([FromQuery] string? query)
        {
            return Ok(await _albumService.FiltrarAlbums(query));
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> AddAlbum([FromBody] AlbumExterno album)
        {
            var nuevo = await _albumService.AddAlbum(album);
            return CreatedAtAction(nameof(GetAlbums), new { id = nuevo.Id }, nuevo);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(int id, [FromBody] AlbumExterno album)
        {
            var actualizado = await _albumService.UpdateAlbum(id, album);
            return actualizado != null ? Ok(actualizado) : NotFound();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var eliminado = await _albumService.DeleteAlbum(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
