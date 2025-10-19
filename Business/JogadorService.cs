using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class JogadorService : IJogadorService
    {
        private readonly ApplicationDbContext _context;

        private readonly ViaCepService _viaCepService;


        public JogadorService(ApplicationDbContext context, ViaCepService viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService;
        }


        public List<Jogador> FindAll()
        {
            return _context.Jogadores.ToList();
        }

        public Jogador FindById(int id)
        {
            return _context.Jogadores.FirstOrDefault(j => j.Id == id);
        }

        public Jogador Create(Jogador jogador)
        {
             _context.Jogadores.Add(jogador);
            _context.SaveChanges();
            return jogador;
        }

        public bool Update(Jogador jogador)
        {
            var existente = _context.Jogadores.Find(jogador.Id);
            if (existente == null) return false;

            existente.Nome = jogador.Nome;
            existente.Email = jogador.Email;
            existente.telefone = jogador.telefone;
            existente.Pontos = jogador.Pontos;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var jogador = _context.Jogadores.Find(id);
            if (jogador == null) return false;

            _context.Jogadores.Remove(jogador);
            _context.SaveChanges();
            return true;
        }

        public bool AtualizarEndereco(int id, string cep)
        {
            var jogador = _context.Jogadores.Find(id);
            if (jogador == null) return false;

            var endereco = _viaCepService.ConsultarEnderecoPorCep(cep).Result;
            if (endereco == null) return false;

            jogador.CEP = cep;
            jogador.Cidade = endereco.Localidade;
            jogador.Estado = endereco.Uf;

            _context.Jogadores.Update(jogador);
            _context.SaveChanges();

            return true;
        }


    }
}
