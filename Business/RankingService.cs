using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class RankingService : IRankingService
    {
        private readonly IJogadorService _jogadorService;

        public RankingService(IJogadorService jogadorService)
        {
            _jogadorService = jogadorService;
        }

        public List<Ranking> GerarRanking()
        {
            var contador = 1;

            return _jogadorService.FindAll()
                .OrderByDescending(j => j.Pontos) // Ordena por pontos decrescente
                .Select(jogador =>
                {
                    var ranking = new Ranking(jogador)
                    {
                        Posicao = contador++
                    };
                    return ranking;
                })
                .ToList();
        }
    }
}
