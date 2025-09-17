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

        public JogadorController(IJogadorService jogadorService)
        {
            _jogadorService = jogadorService;
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
    }
}
