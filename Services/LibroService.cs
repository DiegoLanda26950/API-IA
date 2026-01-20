using LibrosAPI.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace LibrosAPI.Services
{
    public class LibroService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static List<Libro> LibrosLocales = new()
        {
            new Libro { Id = 1, Titulo = "C# Avanzado", Autor = "Juan Pérez" },
            new Libro { Id = 2, Titulo = "ASP.NET Core", Autor = "Ana Gómez" }
        };

        public LibroService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Obtener todos los libros locales
        public List<Libro> GetLibros() => LibrosLocales;

        // Obtener libro por Id
        public Libro? GetLibroById(int id) => LibrosLocales.FirstOrDefault(l => l.Id == id);

        // Agregar un libro
        public Libro AddLibro(Libro libro)
        {
            libro.Id = LibrosLocales.Count + 1;
            LibrosLocales.Add(libro);
            return libro;
        }

        // Actualizar un libro
        public Libro? UpdateLibro(int id, Libro libroActualizado)
        {
            var libro = LibrosLocales.FirstOrDefault(l => l.Id == id);
            if (libro == null) return null;

            libro.Titulo = libroActualizado.Titulo;
            libro.Autor = libroActualizado.Autor;
            return libro;
        }

        // Eliminar un libro
        public bool DeleteLibro(int id)
        {
            var libro = LibrosLocales.FirstOrDefault(l => l.Id == id);
            if (libro == null) return false;
            LibrosLocales.Remove(libro);
            return true;
        }

        // Obtener autores externos desde JSONPlaceholder
        public async Task<List<UsuarioExterno>> GetAutoresExternos()
        {
            var client = _httpClientFactory.CreateClient();
            var usuarios = await client.GetFromJsonAsync<List<UsuarioExterno>>(
                "https://jsonplaceholder.typicode.com/users"
            );
            return usuarios ?? new List<UsuarioExterno>();
        }
    }
}
