using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamer.Data;
using Streamer.Models;

namespace Streamer.Controllers;

[ApiController]
[Route("api/filme")]
public class FilmeController : ControllerBase
{
    private readonly AppDataContext _ctx;
    private readonly IFilmeRepository _repository;
    private readonly ICategoriaRepository _categoriaRepository; // Corrigido
    private readonly IUsuarioRepository _usuarioRepository;     // Corrigido

    public FilmeController(AppDataContext ctx, IFilmeRepository repository, ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository)
    {
        _ctx = ctx;
        _repository = repository;
        _categoriaRepository = categoriaRepository;  // Corrigido
        _usuarioRepository = usuarioRepository;      // Corrigido
    }

    [HttpPost("cadastrar")]
    [Authorize(Roles = "Admin")]
    public IActionResult Cadastrar([FromBody] Filme filme)
{
    if (filme == null)
    {
        return BadRequest("Filme não pode ser nulo");
    }

    var categoria = _categoriaRepository.GetById(filme.CategoriaId); // Chama o método GetById
    var usuario = _usuarioRepository.GetById(filme.UsuarioId);

    // Verifique se a Categoria foi encontrada
    if (categoria == null)
    {
        return BadRequest("Categoria não encontrada.");
    }

    // Verifique se o Usuário foi encontrado
    if (usuario == null)
    {
        return BadRequest("Usuário não encontrado.");
    }

    // Atribua a Categoria e o Usuário ao Filme
    filme.Categoria = categoria;
    filme.Usuario = usuario;

    // Persistir o Filme
    _repository.Add(filme);

    return Ok(filme);
}

    [HttpDelete("deletar/{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Deletar(int id)
    {
        var filme = _repository.BuscarId(id);
        if (filme == null)
        {
            return NotFound(new { mensagem = "Filme não encontrado" });
        }

        _repository.Remover(filme);
        return Ok(new { mensagem = "Filme deletado com sucesso" });
    }
}
