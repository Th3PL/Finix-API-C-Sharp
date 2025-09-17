using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Jogador
    {
        [Key]
        public int Id {  get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string telefone { get; set; }
        public double Pontos { get; set; }
    }
}
