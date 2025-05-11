using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Streamer.Models;

    public class Usuario
    {
         public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public Permissao Permissao { get; set; }= Permissao.Admin;

        public List<Assinatura> Assinaturas { get; set; } = new();
        public List<Filme> FilmesSolicitados { get; set; } = new();
        public List<Comentario> Comentarios { get; set; } = new();
    }