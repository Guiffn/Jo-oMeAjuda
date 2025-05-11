using Streamer.Models;

namespace Streamer.Data
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario usuario);
        List<Usuario> Listar();
        Usuario? BuscarId(int id);
        void Atualizar(Usuario usuario);
        void Remover(Usuario usuario);
        Usuario? BuscarUsuarioPorEmailSenha(string email, string senha);
        Usuario? GetById(int id);
    }
}