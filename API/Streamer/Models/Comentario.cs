using System.ComponentModel.DataAnnotations;
using Streamer.Models;

 public class Comentario
    {
       public int Id { get; set; }
        public string Conteudo { get; set; } = string.Empty;
        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }
        public int FilmeId { get; set; }

        public Usuario Usuario { get; set; } = null!;
        public Filme Filme { get; set; } = null!;
    }