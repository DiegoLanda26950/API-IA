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
        public List<Libro> GetLibros()
        {
            return LibrosLocales;
        }

        // Obtener libro por Id
        public Libro? GetLibroById(int id)
        {
            return LibrosLocales.FirstOrDefault(l => l.Id == id);
        }

        // Agregar un libro
        public Libro AddLibro(Libro libro)
        {
            libro.Id = LibrosLocales.Count + 1;
            LibrosLocales.Add(libro);
            return libro;
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
