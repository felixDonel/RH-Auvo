using RhAuvo.Application.DTOs;
using RhAuvo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Application.Services
{
    public class DepartamentoService
    {
        public DepartamentoService()
        {

        }

        public async Task<Departamento> CriarDepartamento(CsvDTO informacoesCSV)
        {
            Departamento departamento = new Departamento();

            string[] informacoesNome = informacoesCSV.Nome.Split("-");

            departamento.Nome = informacoesNome[0];
            departamento.MesVigente = informacoesNome[1];
            departamento.AnoVigente = int.Parse(informacoesNome[2]);
            if (informacoesCSV.Linhas.Any()) {
                departamento.Funcionarios = await new FuncionarioService().CriarFuncionarios(informacoesCSV.Linhas);
                departamento.CalcularTotalPagar();
                departamento.CalcularTotalDescontos();
                departamento.CalcularTotalExtra();
            }
            return departamento;
        }
    }
}
