using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamer.Data;
using Streamer.Models;
using System.Security.Claims;

namespace Streamer.Controllers
{
    [ApiController]
    [Route("api/comentario")]
    public class ComentarioController : ControllerBase
    {
        private readonly AppDataContext _ctx;
        private readonly IComentarioRepository _repository;

        public ComentarioController(AppDataContext ctx, IComentarioRepository repository)
        {
            _ctx = ctx;
            _repository = repository;
        }

        [HttpPost("cadastrar")]
        [Authorize] // Garante que apenas usuários logados possam comentar
        public IActionResult Cadastrar([FromBody] Comentario comentario)
        {
            // Relacionar o comentário com o usuário logado
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Pegando o ID do usuário do token
            comentario.UsuarioId = usuarioId;

            // Relacionar com filme
            var filme = _ctx.Filmes.Find(comentario.FilmeId);
            if (filme == null)
            {
                return NotFound(new { mensagem = "Filme não encontrado" });
            }

            comentario.Filme = filme;
            _repository.Cadastrar(comentario);

            return Created("", comentario);
        }

        [HttpGet("listar")]
        [AllowAnonymous] // Liberado para todos verem os comentários
        public IActionResult Listar()
        {
            return Ok(_repository.Listar());
        }

        [HttpDelete("deletar/{id}")]
        [Authorize(Roles = "admin")] // Apenas admins podem deletar
        public IActionResult Deletar(int id)
        {
            var comentario = _repository.BuscarId(id);
            if (comentario == null)
            {
                return NotFound(new { mensagem = "Comentário não encontrado" });
            }

            _repository.Remover(comentario);
            return Ok(new { mensagem = "Comentário deletado com sucesso" });
        }
    }
}
