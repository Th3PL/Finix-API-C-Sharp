using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ranking
    {

        public int Posicao {  get; set; }
        public string Nome { get; set; }
        public double Pontos { get; set; }


        public Ranking() { }

        public Ranking(Jogador jogador) 
        {
            Nome = jogador.Nome;
            Pontos = jogador.Pontos;
        }
    }

   
}
