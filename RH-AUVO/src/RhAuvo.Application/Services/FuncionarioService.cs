using Dasync.Collections;
using MoreLinq;
using RhAuvo.Application.DTOs;
using RhAuvo.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Application.Services
{
    public class FuncionarioService
    {
        public FuncionarioService()
        {

        }
        public async Task<List<Funcionario>> CriarFuncionarios(List<LinhaCsvDTO> linhas)
        {
            List<LinhaCsvDTO> linhasFuncionarios = linhas.DistinctBy(l => l.Codigo)
                                                         .ToList();
            List<Funcionario> funcionarios = new List<Funcionario>();
            await linhasFuncionarios.ParallelForEachAsync(async linhaFuncionario =>
            {
                List<DiaFuncionario> dias = await new DiaFuncionarioService().CriarDiasFuncionario(linhas.Where(l => l.Codigo == linhaFuncionario.Codigo)
                                                                                                         .Select(l => new DiaFuncionarioDTO(l.Data, l.Entrada, l.Saida, l.Almoco, l.ValorHora))
                                                                                                         .ToList());
                var funcionario = new Funcionario(linhaFuncionario.Nome, linhaFuncionario.Codigo, dias);

                funcionario.CalcularTotalAReceber();
                funcionario.CalcularDiasFalta();
                funcionario.CalcularDiasTrabalhados();
                funcionario.CalcularSaldosHorasDiarios();
                funcionario.CalcularDiasExtras();

                funcionarios.Add(funcionario);
            }, maxDegreeOfParallelism: 4);
            return funcionarios;
        }
    }
}