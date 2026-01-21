using LibrosAPI.Models;
using System.Net.Http.Json;

namespace LibrosAPI.Services
{
    public class AlbumService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static List<AlbumExterno>? _albumsCache;

        public AlbumService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Cargar albums externos (una sola vez)
        private async Task<List<AlbumExterno>> LoadAlbums()
        {
            if (_albumsCache != null)
                return _albumsCache;

            var client = _httpClientFactory.CreateClient();
            _albumsCache = await client.GetFromJsonAsync<List<AlbumExterno>>(
                "https://jsonplaceholder.typicode.com/albums"
            ) ?? new List<AlbumExterno>();

            return _albumsCache;
        }

        // GET
        public async Task<List<AlbumExterno>> GetAlbums()
        {
            return await LoadAlbums();
        }

        // POST
        public async Task<AlbumExterno> AddAlbum(AlbumExterno album)
        {
            var albums = await LoadAlbums();
            album.Id = albums.Max(a => a.Id) + 1;
            albums.Add(album);
            return album;
        }

        // PUT
        public async Task<AlbumExterno?> UpdateAlbum(int id, AlbumExterno albumActualizado)
        {
            var albums = await LoadAlbums();
            var album = albums.FirstOrDefault(a => a.Id == id);
            if (album == null) return null;

            album.Title = albumActualizado.Title;
            album.UserId = albumActualizado.UserId;
            return album;
        }

        // DELETE
        public async Task<bool> DeleteAlbum(int id)
        {
            var albums = await LoadAlbums();
            var album = albums.FirstOrDefault(a => a.Id == id);
            if (album == null) return false;

            albums.Remove(album);
            return true;
        }

        // FILTRADO
        public async Task<List<AlbumExterno>> FiltrarAlbums(string? query)
        {
            var albums = await LoadAlbums();
            if (string.IsNullOrWhiteSpace(query))
                return albums;

            query = query.ToLower();
            return albums
                .Where(a => a.Title.ToLower().Contains(query))
                .ToList();
        }
    }
}
