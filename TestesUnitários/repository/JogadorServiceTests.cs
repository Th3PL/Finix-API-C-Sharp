using Business;
using Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesUnitários.repository
{

    public class JogadorServiceTests
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private static Jogador NovoJogador(
            int id,
            string nome = "Fulano",
            string email = "fulano@example.com",
            string telefone = "1199999-0000",
            int pontos = 0)
        {
            return new Jogador
            {
                Id = id,
                Nome = nome,
                Email = email,
                telefone = telefone,
                Pontos = pontos
            };
        }

        [Fact]
        public void FindAll_DeveRetornarTodosOsJogadores()
        {
            using var ctx = CreateContext();
            ctx.Jogadores.AddRange(
                NovoJogador(1, "Ana"),
                NovoJogador(2, "Bruno", pontos: 10)
            );
            ctx.SaveChanges();

            var service = new JogadorService(ctx);

            var lista = service.FindAll();

            lista.Should().NotBeNull();
            lista.Should().HaveCount(2);
            lista.Select(j => j.Nome).Should().Contain(new[] { "Ana", "Bruno" });
        }

        [Fact]
        public void FindById_DeveRetornarJogador_QuandoExistir()
        {
            using var ctx = CreateContext();
            ctx.Jogadores.AddRange(
                NovoJogador(1, "Ana"),
                NovoJogador(2, "Bruno")
            );
            ctx.SaveChanges();

            var service = new JogadorService(ctx);

            var jogador = service.FindById(2);

            jogador.Should().NotBeNull();
            jogador!.Id.Should().Be(2);
            jogador.Nome.Should().Be("Bruno");
        }

        [Fact]
        public void FindById_DeveRetornarNull_QuandoNaoExistir()
        {
            using var ctx = CreateContext();
            ctx.Jogadores.Add(NovoJogador(1, "Ana"));
            ctx.SaveChanges();

            var service = new JogadorService(ctx);

            var jogador = service.FindById(99);

            jogador.Should().BeNull();
        }

        [Fact]
        public void Create_DeveAdicionarJogadorESalvar()
        {
            using var ctx = CreateContext();
            var service = new JogadorService(ctx);

            var novo = new Jogador
            {
                Id = 10, // Se sua PK for identity, você pode deixar 0 e checar a geração
                Nome = "Carlos",
                Email = "carlos@example.com",
                telefone = "1191111-2222",
                Pontos = 5
            };

            var criado = service.Create(novo);

            criado.Should().NotBeNull();
            criado.Id.Should().Be(10);
            ctx.Jogadores.Count().Should().Be(1);

            var noBanco = ctx.Jogadores.First();
            noBanco.Nome.Should().Be("Carlos");
            noBanco.Pontos.Should().Be(5);
        }

        [Fact]
        public void Update_DeveRetornarFalse_QuandoJogadorNaoExistir()
        {
            using var ctx = CreateContext();
            var service = new JogadorService(ctx);

            var alteracao = new Jogador
            {
                Id = 99,
                Nome = "Fantasma",
                Email = "x@example.com",
                telefone = "0000",
                Pontos = 10
            };

            var ok = service.Update(alteracao);

            ok.Should().BeFalse();
            ctx.Jogadores.Should().BeEmpty();
        }

        [Fact]
        public void Update_DeveAtualizarCampos_QuandoJogadorExistir()
        {
            using var ctx = CreateContext();
            ctx.Jogadores.Add(NovoJogador(1, "Ana", "ana@old.com", "1111", 0));
            ctx.SaveChanges();

            var service = new JogadorService(ctx);

            var alteracao = new Jogador
            {
                Id = 1,
                Nome = "Ana Paula",
                Email = "ana@new.com",
                telefone = "2222",
                Pontos = 42
            };

            var ok = service.Update(alteracao);

            ok.Should().BeTrue();

            var atualizado = ctx.Jogadores.Find(1)!;
            atualizado.Nome.Should().Be("Ana Paula");
            atualizado.Email.Should().Be("ana@new.com");
            atualizado.telefone.Should().Be("2222");
            atualizado.Pontos.Should().Be(42);
        }

        [Fact]
        public void Delete_DeveRetornarFalse_QuandoJogadorNaoExistir()
        {
            using var ctx = CreateContext();
            ctx.Jogadores.Add(NovoJogador(1, "Ana"));
            ctx.SaveChanges();

            var service = new JogadorService(ctx);

            var ok = service.Delete(99);

            ok.Should().BeFalse();
            ctx.Jogadores.Should().HaveCount(1);
        }

        [Fact]
        public void Delete_DeveRemover_QuandoJogadorExistir()
        {
            using var ctx = CreateContext();
            ctx.Jogadores.AddRange(NovoJogador(1, "Ana"), NovoJogador(2, "Bruno"));
            ctx.SaveChanges();

            var service = new JogadorService(ctx);

            var ok = service.Delete(1);

            ok.Should().BeTrue();
            ctx.Jogadores.Should().HaveCount(1);
            ctx.Jogadores.First().Id.Should().Be(2);
        }
    }

}
