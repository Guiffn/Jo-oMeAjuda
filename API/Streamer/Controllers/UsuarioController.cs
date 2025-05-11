using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Streamer.Data;
using Streamer.Models;


namespace Streamer.Controllers;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IFilmeRepository _filmeRepository;
    private readonly AppDataContext _context;
    private readonly IConfiguration _configuration;

    public UsuarioController(
        IUsuarioRepository usuarioRepository,
        IFilmeRepository filmeRepository,
        AppDataContext context,
        IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _filmeRepository = filmeRepository;
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        var usuarioExistente= _context.Usuarios.FirstOrDefault(x => x.Email.ToLower() == usuario.Email.ToLower());
        if(usuarioExistente!=null){
            return BadRequest(new{mensagem="Usuário existente tente novamente"});
        }
        _usuarioRepository.Cadastrar(usuario);
        return Created("", usuario);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario usuario)
    {
        Usuario? usuarioExistente = _usuarioRepository
            .BuscarUsuarioPorEmailSenha(usuario.Email, usuario.Senha);

        if (usuarioExistente == null)
        {
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos!" });
        }

        string token = GerarToken(usuarioExistente);
        return Ok(token);
    }

    [HttpGet("listar")]
    public IActionResult Listar()
    {
        return Ok(_usuarioRepository.Listar());
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var usuario = _usuarioRepository.BuscarId(id);
        if (usuario == null)
            return NotFound(new { mensagem = "Usuário não encontrado" });

        return Ok(usuario);
    }

    [HttpPut("atualizar/{id}")]
    public IActionResult Atualizar(int id, [FromBody] Usuario usuarioAlterado)
    {
        var usuario = _usuarioRepository.BuscarId(id);
        if (usuario == null)
            return NotFound(new { mensagem = "Usuário não encontrado" });

        usuario.Nome = usuarioAlterado.Nome;
        usuario.Email = usuarioAlterado.Email;
        usuario.Senha = usuarioAlterado.Senha;
        usuario.Permissao = usuarioAlterado.Permissao;

        _usuarioRepository.Atualizar(usuario);
        return Ok(usuario);
    }

    [HttpDelete("deletar/{id}")]
    public IActionResult Deletar(int id)
    {
        var usuario = _usuarioRepository.BuscarId(id);
        if (usuario == null)
            return NotFound(new { mensagem = "Usuário não encontrado" });

        _usuarioRepository.Remover(usuario);
        return Ok(new { mensagem = "Usuário deletado com sucesso" });
    }

    [HttpPost("{usuarioId}/solicitar-filme/{filmeId}")]
    public IActionResult SolicitarFilme(int usuarioId, int filmeId)
    {
        var usuario = _usuarioRepository.BuscarId(usuarioId);
        if (usuario == null)
            return NotFound(new { mensagem = "Usuário não encontrado" });

        var filme = _filmeRepository.BuscarId(filmeId);
        if (filme == null)
            return NotFound(new { mensagem = "Filme não encontrado" });

        filme.UsuarioId = usuario.Id;
        _filmeRepository.Atualizar(filme);

        return Ok(new { mensagem = "Filme solicitado com sucesso", filme });
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public string GerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Permissao.ToString())
        };

        var chave = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);

        var assinatura = new SigningCredentials(
            new SymmetricSecurityKey(chave),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: assinatura
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
