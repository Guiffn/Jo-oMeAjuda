using Streamer.Models;

namespace Streamer.Data;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDataContext _ctx;

    public CategoriaRepository(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    public void CadastrarCate(Categoria categoria)
    {
        _ctx.Categorias.Add(categoria);
        _ctx.SaveChanges(); 
    }

    public List<Categoria> ListarCate()
    {
        return _ctx.Categorias.ToList();
    }

    public Categoria? BuscarCategoria(int id)
    {
        return _ctx.Categorias.FirstOrDefault(x => x.Id == id);
    }

    public void AtualizarCate(Categoria categoria)
    {
        _ctx.Categorias.Update(categoria);
        _ctx.SaveChanges(); 
    }
    
    public Categoria GetById(int id)
    {
        return _ctx.Categorias.FirstOrDefault(c => c.Id == id);
    }
}
