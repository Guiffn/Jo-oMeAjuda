using System.Text.Json.Serialization;  
using System.ComponentModel.DataAnnotations;
using Streamer.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Filme
{
 public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        [ValidateNever] 
        [JsonIgnore]        
        public Categoria? Categoria { get; set; }
        
        [ValidateNever]
        [JsonIgnore]        
        public Usuario? Usuario { get; set; }
        public List<Comentario> Comentarios { get; set; } = new();
}
