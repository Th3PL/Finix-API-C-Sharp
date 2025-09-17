using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IJogadorService
    {
        List<Jogador> FindAll();
        Jogador FindById(int id);
        Jogador Create(Jogador jogador);
        bool Update(Jogador jogador);
        bool Delete(int id);

    }
}
