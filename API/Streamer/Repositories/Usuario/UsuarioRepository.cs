using Streamer.Models;

namespace Streamer.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDataContext _ctx;

        public UsuarioRepository(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        public void Cadastrar(Usuario usuario)
        {
            _ctx.Usuarios.Add(usuario);
            _ctx.SaveChanges();
        }

        public List<Usuario> Listar()
        {
            return _ctx.Usuarios.ToList();
        }

        public Usuario? BuscarId(int id)
        {
            return _ctx.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public void Atualizar(Usuario usuario)
        {
            _ctx.Usuarios.Update(usuario);
            _ctx.SaveChanges();
        }

        public void Remover(Usuario usuario)
        {
            _ctx.Usuarios.Remove(usuario);
            _ctx.SaveChanges();
        }
         public Usuario? BuscarUsuarioPorEmailSenha(string email, string senha)
    {
        Usuario? usuarioExistente = 
            _ctx.Usuarios.FirstOrDefault
            (x => x.Email == email && x.Senha == senha);
        return usuarioExistente;
    }
     public Usuario GetById(int id)
    {
        return _ctx.Usuarios.FirstOrDefault(u => u.Id == id);
    }

    }
}
