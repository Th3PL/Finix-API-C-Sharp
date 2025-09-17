using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ranking
    {

        public int posicao {  get; set; }
        public string nome { get; set; }
        public double pontos { get; set; }



        public Ranking(Jogador jogador) 
        {
            nome = jogador.Nome;
            pontos = jogador.Pontos;
        }
    }

   
}
