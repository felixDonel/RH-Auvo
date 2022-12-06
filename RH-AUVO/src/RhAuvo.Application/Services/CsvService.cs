using RhAuvo.Application.DTOs;
using RhAuvo.Data;
using RhAuvo.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RhAuvo.Application.Helpers;
using Dasync.Collections;
using System.IO;
using System.Text.Json;

namespace RhAuvo.Application.Services
{
    public class CsvService
    {
        public CsvService()
        {
        }

        public async Task<string> FormatacaoJson(string caminho) 
        {
            //Tem o pacote CsvHelper
            List<Csv> planilhas = await new CsvRepository().BuscarCsv(caminho);
            List<DepartamentoDTO> departamentos = new List<DepartamentoDTO>();
            await planilhas.ParallelForEachAsync(async planilha =>
            {
                CsvDTO csv = new CsvDTO { 
                    Nome = planilha.Nome, 
                    Linhas = await ParseLinhaCsvDTO(planilha.Conteudo.ToArray())
                };
                Departamento departamento = await new DepartamentoService().CriarDepartamento(csv);
                departamentos.Add(await ParseDepartamentoDTO(departamento));
            },maxDegreeOfParallelism:4);
            string departamentoEmStringJson = JsonSerializer.Serialize(departamentos);
            StreamWriter sw = new StreamWriter($"{caminho}\\Retorno.json", true, Encoding.ASCII);
            
            sw.Write(departamentoEmStringJson);

            //close the file
            sw.Close();

            return departamentoEmStringJson;
        }

        private async Task<DepartamentoDTO> ParseDepartamentoDTO(Departamento departamento)
        {
            return new DepartamentoDTO()
            {
                Departamento = departamento.Nome,
                AnoVigencia = departamento.AnoVigente,
                MesVigencia = departamento.MesVigente,
                TotalPagar = departamento.TotalPagar,
                TotalDesconto = departamento.TotalDescontos,
                TotalExtras = departamento.TotalExtra,
                Funcionarios = departamento.Funcionarios.Select(f => new FuncionarioDTO { Nome = f.Nome, Codigo = f.Codigo, TotalReceber = f.TotalAReceber, HoraExtras = f.HorasExtras, HorasDebito = f.HorasFaltantes, DiasExtras = f.DiasExtras, DiasFalta = f.DiasFalta, DiasTrabalhados = f.DiasTrabalhados }).ToList()
            };
        }

        private async Task<List<LinhaCsvDTO>> ParseLinhaCsvDTO(string[] conteudoDaPlanilha)
        {
            List<(string, int)> ordemDaPlanilha = OrdemDoCabecalho(conteudoDaPlanilha[0].Split(";"));;
            var linhas = new List<LinhaCsvDTO>();
            for (var i = 1; conteudoDaPlanilha.Length > i; i++)
            {
                LinhaCsvDTO linhaDto = new LinhaCsvDTO();
                var linhaCsv = conteudoDaPlanilha[i].Split(";");
                linhaDto.Linha = i;
                linhaDto.Codigo = int.Parse(linhaCsv[IndiceDaPlanilha(ordemDaPlanilha, PropertyHelpers.GetPropertyDisplayName<LinhaCsvDTO>(i => i.Codigo))]);
                linhaDto.Nome = linhaCsv[IndiceDaPlanilha(ordemDaPlanilha, PropertyHelpers.GetPropertyDisplayName<LinhaCsvDTO>(i => i.Nome))];
                linhaDto.ValorHora = RetornarValorHora(linhaCsv[IndiceDaPlanilha(ordemDaPlanilha, PropertyHelpers.GetPropertyDisplayName<LinhaCsvDTO>(i => i.ValorHora))]);
                linhaDto.Data = linhaCsv[IndiceDaPlanilha(ordemDaPlanilha, PropertyHelpers.GetPropertyDisplayName<LinhaCsvDTO>(i => i.Data))];
                linhaDto.Entrada = linhaCsv[IndiceDaPlanilha(ordemDaPlanilha, PropertyHelpers.GetPropertyDisplayName<LinhaCsvDTO>(i => i.Entrada))];
                linhaDto.Saida = linhaCsv[IndiceDaPlanilha(ordemDaPlanilha, PropertyHelpers.GetPropertyDisplayName<LinhaCsvDTO>(i => i.Saida))];
                linhaDto.Almoco = linhaCsv[IndiceDaPlanilha(ordemDaPlanilha, PropertyHelpers.GetPropertyDisplayName<LinhaCsvDTO>(i => i.Almoco))];
                linhas.Add(linhaDto);
            }
            return linhas;
        }


        private List<(string, int)> OrdemDoCabecalho(string[] Cabecalho)
        {
            List<(string, int)> Ordem = new List<(string, int)>();
            for (var i = 0; Cabecalho.Length > i; i++)
            {
                Ordem.Add((Cabecalho[i], i));
            }
            return Ordem;
        }

        private double RetornarValorHora(string valor) {
         return double.Parse(valor.Substring(3).Replace(',', '.').Replace(" ", ""));
        }

        private int IndiceDaPlanilha(List<(string, int)> ordemDaPlanilha, string propriedade)
        {
            return ordemDaPlanilha.Where(o => o.Item1 == propriedade)
                                  .Select(o => o.Item2)
                                  .FirstOrDefault();
        }

    }
}
