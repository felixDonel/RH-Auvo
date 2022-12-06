using RhAuvo.Application.DTOs;
using RhAuvo.Application.Services;
using RhAuvo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RhAuvo.Tests
{
    public class FuncionarioTeste
    {
        [Fact]
        public async Task Funcionario_CalcularTotalAReceber_RetornarSomaDosDiasFuncionario()
        {
            // Arrange
            List<DiaFuncionario> dias = new List<DiaFuncionario>();
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now, HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-1), HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-2), HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-3), HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-4), HoraTrabalhada = 10 });
            var funcionario = new Domain.Funcionario("Felix Donel", 8, dias);

            //Act
            funcionario.CalcularTotalAReceber();
            //assert
            Assert.Equal(funcionario.TotalAReceber, 5000);

        }
        [Fact]
        public async Task Funcionario_CalcularDiasFalta_RetornarDiasSemHorasTrabalhadas()
        {
            // Arrange
            List<DiaFuncionario> dias = new List<DiaFuncionario>();
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now, HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-1), HoraTrabalhada = 0 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-2), HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-3), HoraTrabalhada = 0 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-4), HoraTrabalhada = 10 });
            var funcionario = new Domain.Funcionario("Felix Donel", 8, dias);

            //Act
            funcionario.CalcularDiasFalta();

            //assert
            Assert.Equal(funcionario.DiasFalta, 2);

        }
    }
}
