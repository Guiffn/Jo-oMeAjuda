using Microsoft.EntityFrameworkCore;
using Streamer.Models;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options) { }

    public DbSet<Filme>Filmes{ get; set; }
    public DbSet<Usuario>Usuarios{ get; set; }
    public DbSet<Comentario>Comentarios{ get; set; }
    public DbSet<Assinatura>Assinaturas { get; set; }
    public DbSet<Categoria>Categorias  { get; set; }
    public DbSet<SolicitarFilme> Solicitacoes { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Filme>()
        .HasOne(f => f.Categoria)
        .WithMany(c => c.Filmes)
        .HasForeignKey(f => f.CategoriaId);

    modelBuilder.Entity<Comentario>()
        .HasOne(c => c.Usuario)
        .WithMany(u => u.Comentarios)
        .HasForeignKey(c => c.UsuarioId);

    modelBuilder.Entity<Comentario>()
        .HasOne(c => c.Filme)
        .WithMany(f => f.Comentarios)
        .HasForeignKey(c => c.FilmeId);

    modelBuilder.Entity<Assinatura>()
        .HasOne(a => a.Usuario)
        .WithMany(u => u.Assinaturas)
        .HasForeignKey(a => a.UsuarioId);

    modelBuilder.Entity<SolicitarFilme>()
        .HasOne(s => s.Usuario)
        .WithMany()
        .HasForeignKey(s => s.UsuarioId);

    modelBuilder.Entity<SolicitarFilme>()
        .HasOne(s => s.Filme)
        .WithMany()
        .HasForeignKey(s => s.FilmeId);
}



}
