using Business;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("finix/[controller]")]
    public class JogadorController : ControllerBase
    {
        private readonly IJogadorService _jogadorService;

        private readonly ViaCepService _viaCepService;


        public JogadorController(IJogadorService jogadorService, ViaCepService viaCepService)
        {
            _jogadorService = jogadorService;
            _viaCepService = viaCepService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var jogadores = _jogadorService.FindAll();
            return jogadores.Count == 0 ? NoContent() : Ok(jogadores);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var jogador = _jogadorService.FindById(id);
            return jogador == null ? NotFound() : Ok(jogador);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Jogador jogador)
        {
            if (string.IsNullOrWhiteSpace(jogador.Nome))
                return BadRequest("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(jogador.Email))
                return BadRequest("Email é obrigatório.");

            var criado = _jogadorService.Create(jogador);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Jogador jogador)
        {
            if (jogador == null)
                return BadRequest("Dados inconsistentes.");

            return _jogadorService.Update(jogador) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _jogadorService.Delete(id) ? NoContent() : NotFound();
        }


        [HttpPut("atualizar-endereco/{id}")]
        public IActionResult AtualizarEndereco(int id, [FromBody] string cep)
        {
            var atualizado = _jogadorService.AtualizarEndereco(id, cep);
            return atualizado ? Ok("Endereço atualizado com sucesso.") : NotFound("Jogador não encontrado ou erro ao consultar CEP.");
        }

    }
}
