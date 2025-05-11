using System;
using Microsoft.EntityFrameworkCore;
using Streamer.Models;

namespace Streamer.Repositories.SolicitarFilmes;

public class SolicitarFilmeRepository : ISolicitarFilmeRepository
{
 private readonly AppDataContext _context;

        public SolicitarFilmeRepository(AppDataContext context)
        {
            _context = context;
        }

        public void Cadastrar(SolicitarFilme solicitarFilme)
        {
            _context.Solicitacoes.Add(solicitarFilme);
            _context.SaveChanges();
        }

        public List<SolicitarFilme> Listar()
        {
            return _context.Solicitacoes.Include(s => s.Usuario).Include(s => s.Filme).ToList();
        }

        public SolicitarFilme BuscarPorId(int id)
        {
            return _context.Solicitacoes.Include(s => s.Usuario).Include(s => s.Filme)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Remover(SolicitarFilme solicitarFilme)
        {
            _context.Solicitacoes.Remove(solicitarFilme);
            _context.SaveChanges();
        }
}
