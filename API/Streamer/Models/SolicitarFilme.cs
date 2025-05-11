using System;

namespace Streamer.Models;

public class SolicitarFilme
{

     public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public int FilmeId { get; set; }
        public Filme Filme { get; set; } = null!;

        public bool Atendida { get; set; } = false;         
        public DateTime DataSolicitacao { get; set; } = DateTime.Now;
}

