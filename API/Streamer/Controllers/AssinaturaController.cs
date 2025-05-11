using Microsoft.AspNetCore.Mvc;
using Streamer.Data;
using Streamer.Models;

namespace API_STREAMER.Streamer.Controllers
{
    [Route("api/assinatura")]
    [ApiController]
    public class AssinaturaController : ControllerBase
    {
        private readonly AppDataContext _ctx;
        private readonly IAssinaturaRepository _repository;

        public AssinaturaController(AppDataContext ctx, IAssinaturaRepository repository)
        {
            _ctx = ctx;
            _repository = repository;
        }

        // Cadastrar assinatura
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] Assinatura assinatura)
        {
            // Verificar se o usuário já tem uma assinatura ativa
            var usuarioExistente = _ctx.Usuarios.FirstOrDefault(u => u.Id == assinatura.UsuarioId);
            if (usuarioExistente == null)
            {
                return NotFound(new { mensagem = "Usuário não encontrado" });
            }

            // Cadastrar assinatura
            _repository.Cadastrar(assinatura);
            return Created("", assinatura);
        }

        // Listar todas as assinaturas
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            var assinaturas = _repository.Listar();
            if (assinaturas == null || assinaturas.Count == 0)
            {
                return NotFound(new { mensagem = "Nenhuma assinatura encontrada" });
            }

            return Ok(assinaturas);
        }

        // Deletar assinatura
        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            var assinatura = _repository.BuscarId(id);
            if (assinatura == null)
            {
                return NotFound(new { mensagem = "Assinatura não encontrada" });
            }

            _repository.Remover(assinatura);
            return Ok();
        }
    }
}
