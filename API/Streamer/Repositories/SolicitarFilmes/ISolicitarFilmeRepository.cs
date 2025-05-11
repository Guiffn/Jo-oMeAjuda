using System;
using Streamer.Models;

namespace Streamer.Repositories.SolicitarFilmes;

public interface ISolicitarFilmeRepository
{
void Cadastrar(SolicitarFilme solicitarFilme);
        List<SolicitarFilme> Listar();
        SolicitarFilme BuscarPorId(int id);
        void Remover(SolicitarFilme solicitarFilme);
}
