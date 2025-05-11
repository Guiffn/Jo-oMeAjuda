using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streamer.Models;
using Streamer.Repositories.SolicitarFilmes;


namespace Streamer.Controllers;

    [Route("api/solicitar-filme")]
    [ApiController]
    public class SolicitarFilmeController : ControllerBase
    {
        private readonly ISolicitarFilmeRepository _repository;
        private readonly AppDataContext _ctx;

        public SolicitarFilmeController(ISolicitarFilmeRepository repository, AppDataContext ctx)
        {
            _repository = repository;
            _ctx = ctx;
        }

        // Cadastrar solicitação de filme
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] SolicitarFilme solicitarFilme)
        {
            // Verificar se o filme e o usuário existem
            var filme = _ctx.Filmes.Find(solicitarFilme.FilmeId);
            if (filme == null)
            {
                return NotFound(new { mensagem = "Filme não encontrado" });
            }

            var usuario = _ctx.Usuarios.Find(solicitarFilme.UsuarioId);
            if (usuario == null)
            {
                return NotFound(new { mensagem = "Usuário não encontrado" });
            }

            // Cadastrar a solicitação de filme
            _repository.Cadastrar(solicitarFilme);
            return Created("", solicitarFilme);
        }

        // Listar todas as solicitações de filmes
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            var solicitacoes = _repository.Listar();
            if (solicitacoes == null || solicitacoes.Count == 0)
            {
                return NotFound(new { mensagem = "Nenhuma solicitação encontrada" });
            }
            return Ok(solicitacoes);
        }

        // Buscar uma solicitação de filme pelo ID
        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            var solicitacao = _repository.BuscarPorId(id);
            if (solicitacao == null)
            {
                return NotFound(new { mensagem = "Solicitação de filme não encontrada" });
            }
            return Ok(solicitacao);
        }

        // Deletar uma solicitação de filme
        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            var solicitacao = _repository.BuscarPorId(id);
            if (solicitacao == null)
            {
                return NotFound(new { mensagem = "Solicitação de filme não encontrada" });
            }

            _repository.Remover(solicitacao);
            return Ok();
        }
    }


