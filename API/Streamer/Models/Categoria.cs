using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Streamer.Models
{
     public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Filme> Filmes { get; set; } = new();
    }
}
