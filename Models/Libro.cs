namespace LibrosAPI.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
    }

    public class UsuarioExterno
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
    }
}
