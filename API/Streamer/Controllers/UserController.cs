using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Streamer.Models;
using Streamer.Repositories.Users;

namespace Streamer.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUsersRepository _usersRepository;

    public UserController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [HttpPost()]
    [AllowAnonymous]
    public IActionResult Create([FromBody] User user)
    {
        var userExistente = _usersRepository.GetByEmail(user.Email);
        if(userExistente != null){
            return BadRequest(new{mensagem="Usuário existente tente novamente"});
        }
        _usersRepository.Create(user);
        return Created("", user);
    }

    [HttpGet()]
    public IActionResult List()
    {
        var users = _usersRepository.List();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var user = _usersRepository.Get(id);
        if (user == null) {
            return NotFound(new { mensagem = "Usuário não encontrado" });
        }
            
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UserUpdateDto userAtualizado)
    {
        var userExistente = _usersRepository.Get(id);
        if (userExistente == null)
        {
            return NotFound(new { mensagem = "Usuário não encontrado" });
        }

        if (!string.IsNullOrWhiteSpace(userAtualizado.Name))
        {
            userExistente.Name = userAtualizado.Name;
        }

        if (!string.IsNullOrWhiteSpace(userAtualizado.Email))
        {
            userExistente.Email = userAtualizado.Email;
        }

        if (!string.IsNullOrWhiteSpace(userAtualizado.Password))
        {
            userExistente.Password = userAtualizado.Password;
        }

        _usersRepository.Update(userExistente);
        return Ok(userExistente);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = _usersRepository.Get(id);
        if (user == null)
            return NotFound(new { mensagem = "Usuário não encontrado" });
            
        _usersRepository.Delete(id);
        return NoContent();
    }
}
