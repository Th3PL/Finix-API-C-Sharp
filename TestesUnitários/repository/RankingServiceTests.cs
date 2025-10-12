using Business;
using FluentAssertions;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesUnitários.repository
{


    public class RankingServiceTests
    {
        private static Jogador J(int id, string nome, int pontos) =>
            new Jogador
            {
                Id = id,
                Nome = nome,
                Email = $"{nome.ToLower()}@example.com",
                telefone = "1199999-0000",
                Pontos = pontos
            };

        [Fact]
        public void GerarRanking_DeveOrdenarPorPontosDescEAtribuirPosicoes()
        {
            var mockJogadorService = new Mock<IJogadorService>();
            mockJogadorService
                .Setup(s => s.FindAll())
                .Returns(new List<Jogador>
                {
                    J(1, "Ana",   50),
                    J(2, "Bruno",120),
                    J(3, "Carla", 75)
                });

            var sut = new RankingService(mockJogadorService.Object);

            var ranking = sut.GerarRanking();

            ranking.Should().NotBeNull();
            ranking.Should().HaveCount(3);

            ranking.Select(r => r.Posicao).Should().ContainInOrder(1, 2, 3);
            ranking.Select(r => r.Nome).Should().ContainInOrder("Bruno", "Carla", "Ana");
            ranking.Select(r => r.Pontos).Should().BeInDescendingOrder();

            mockJogadorService.Verify(s => s.FindAll(), Times.Once);
            mockJogadorService.VerifyNoOtherCalls();
        }

        [Fact]
        public void GerarRanking_DeveManterOrdemEstavel_QuandoEmpateDePontos()
        {
            var mockJogadorService = new Mock<IJogadorService>();
            mockJogadorService
                .Setup(s => s.FindAll())
                .Returns(new List<Jogador>
                {
                    J(1, "Ana",   100),
                    J(2, "Bruno", 100),
                    J(3, "Carla",  50)
                });

            var sut = new RankingService(mockJogadorService.Object);

            var ranking = sut.GerarRanking();

            ranking.Should().HaveCount(3);

           
            ranking.Select(r => r.Nome)
                   .Should().ContainInOrder("Ana", "Bruno", "Carla");

            ranking.Select(r => r.Posicao)
                   .Should().ContainInOrder(1, 2, 3);

            ranking.Take(2).Select(r => r.Pontos).Should().OnlyContain(p => p == 100);
        }

        [Fact]
        public void GerarRanking_DeveRetornarListaVazia_QuandoNaoHaJogadores()
        {
        
            var mockJogadorService = new Mock<IJogadorService>();
            mockJogadorService.Setup(s => s.FindAll()).Returns(new List<Jogador>());

            var sut = new RankingService(mockJogadorService.Object);

      
            var ranking = sut.GerarRanking();

            ranking.Should().NotBeNull().And.BeEmpty();
            mockJogadorService.Verify(s => s.FindAll(), Times.Once);
        }

        [Fact]
        public void GerarRanking_DeveAtribuirPosicoesSequenciais_IndependenteDaQuantidade()
        {
            var jogadores = new List<Jogador>
            {
                J(4, "J4", 40),
                J(8, "J8", 80),
                J(1, "J1", 10),
                J(10,"J10",100),
                J(6, "J6", 60)
            };

            var mockJogadorService = new Mock<IJogadorService>();
            mockJogadorService.Setup(s => s.FindAll()).Returns(jogadores);

            var sut = new RankingService(mockJogadorService.Object);

            var ranking = sut.GerarRanking();

            ranking.Should().HaveCount(jogadores.Count);

            ranking.Select(r => r.Posicao).Should().ContainInOrder(1, 2, 3, 4, 5);
            ranking.Select(r => r.Pontos).Should().BeInDescendingOrder();
            ranking.Select(r => r.Nome).Should().ContainInOrder("J10", "J8", "J6", "J4", "J1");
        }
    }


}
