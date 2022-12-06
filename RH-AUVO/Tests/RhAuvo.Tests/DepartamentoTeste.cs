using RhAuvo.Application.DTOs;
using RhAuvo.Application.Services;
using RhAuvo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RhAuvo.Tests
{
    public class DepartamentoTeste
    {
        [Fact]
        public async Task Departamento_CriarDepartamento_RetornarDepartamentoNomeCerto()
        {
            // Arrange
            string NomeDoDoc = "Departamento de Operações Despeciais-Abril-2022";
            string nomeEsperado = NomeDoDoc.Split("-")[0];

            //Act
            Departamento departamento = await new DepartamentoService().CriarDepartamento(new CsvDTO() { Nome = NomeDoDoc, Linhas = new List<LinhaCsvDTO>() });

            //assert
            Assert.Equal(departamento.Nome, nomeEsperado);

        }
        [Fact]
        public async Task Departamento_CriarDepartamento_RetornarDepartamentoMesVigenteCerto()
        {
            // Arrange
            string NomeDoDoc = "Departamento de Operações Despeciais-Abril-2022";
            string nomeEsperado = NomeDoDoc.Split("-")[1];

            //Act
            Departamento departamento = await new DepartamentoService().CriarDepartamento(new CsvDTO() { Nome = NomeDoDoc, Linhas = new List<LinhaCsvDTO>() });

            //assert
            Assert.Equal(departamento.MesVigente, nomeEsperado);

        }
        [Fact]
        public async Task Departamento_CriarDepartamento_RetornarDepartamentoAnoVigente()
        {
            // Arrange
            string NomeDoDoc = "Departamento de Operações Despeciais-Abril-2022";
            int nomeEsperado = int.Parse(NomeDoDoc.Split("-")[2]);

            //Act
            Departamento departamento = await new DepartamentoService().CriarDepartamento(new CsvDTO() { Nome = NomeDoDoc,Linhas = new List<LinhaCsvDTO>()});

            //assert
            Assert.Equal(departamento.AnoVigente, nomeEsperado);

        }

        [Fact]
        public async Task Departamento_CalcularTotalPagar_RetornarCalcularTotalPagar()
        {
            // Arrange
            Departamento departamento = new Departamento();
            List<Funcionario> funcionarios = new List<Funcionario>();
            List<DiaFuncionario> dias = new List<DiaFuncionario>();
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now, HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-1), HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-2), HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-3), HoraTrabalhada = 10 });
            dias.Add(new DiaFuncionario() { ValorHora = 100, dia = DateTime.Now.AddDays(-4), HoraTrabalhada = 10 });
            funcionarios.Add(new Domain.Funcionario("Felix Donel", 8, dias));
            funcionarios.Add(new Domain.Funcionario("Felix 2 Donel", 8, dias));
            funcionarios.ForEach(f => f.CalcularTotalAReceber());
            departamento.Funcionarios = funcionarios;

            //Act
            departamento.CalcularTotalPagar();

            //assert
            Assert.Equal(departamento.TotalPagar, 10000);

        }
    }
}
