using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Domain
{
    public class Funcionario : Entity
    {
        public Guid DepartamentoId { get; set; }
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public double TotalAReceber { get; set; }
        public double HorasExtras { get; set; }
        public double HorasFaltantes { get; set; }
        public int DiasFalta { get; set; }
        public int DiasExtras { get; set; }
        public int DiasTrabalhados { get; set; }
        public double ValorDescontos { get; set; }
        public double ValorExtra { get; set; }

        public virtual Departamento Departamento { get; set; }

        public List<DiaFuncionario> DiasFuncionario { get; set; }
        public Funcionario()
        {

        }
        public Funcionario(string nome, int codigo, List<DiaFuncionario> diasFuncionario)
        {
            Nome = nome;
            Codigo = codigo;
            DiasFuncionario = diasFuncionario;
        }

        public void CalcularTotalAReceber()
        {
            DiasFuncionario.Select(d => d.HoraTrabalhada * DiasFuncionario.Where(l => l.dia == d.dia)
                                                        .Select(l => l.ValorHora)
                                                        .FirstOrDefault())
                    .ToList()
                    .ForEach(v => TotalAReceber += v);
        }

        public void CalcularDiasFalta()
        {
            DiasFalta = DiasFuncionario.Where(d => d.dia.DayOfWeek != DayOfWeek.Sunday || d.dia.DayOfWeek != DayOfWeek.Saturday)
                                       .Where(d => d.HoraTrabalhada < 1)
                                       .Count();
        }
        public void CalcularDiasTrabalhados()
        {
            DiasTrabalhados = DiasFuncionario.Where(d => d.HoraTrabalhada > 0).Count();
        }
        public void CalcularSaldosHorasDiarios()
        {
            DiasFuncionario.Where(d => d.dia.DayOfWeek != DayOfWeek.Sunday && d.dia.DayOfWeek != DayOfWeek.Saturday)
                           .Select(d => (d.HoraTrabalhada - d.HorasAcordadas, d.ValorHora))
                           .ToList()
                           .ForEach(horasExtras =>
                           {
                               if (horasExtras.Item1 > 0)
                               {
                                   HorasExtras += horasExtras.Item1;
                                   ValorExtra += horasExtras.Item1 * horasExtras.Item2;
                               }
                               else
                               {
                                   HorasFaltantes += -horasExtras.Item1;
                                   ValorDescontos += horasExtras.Item1 * horasExtras.Item2;
                               }

                           });
            DiasFuncionario.Where(d => d.dia.DayOfWeek == DayOfWeek.Sunday || d.dia.DayOfWeek == DayOfWeek.Saturday)
                            .Select(d => d.HoraTrabalhada)
                            .ToList()
                            .ForEach(horas => { HorasExtras += horas; });

        }

        public void CalcularDiasExtras()
        {
            DiasExtras = DiasFuncionario.Where(d => d.dia.DayOfWeek == DayOfWeek.Sunday || d.dia.DayOfWeek == DayOfWeek.Saturday)
                                        .Count();
        }
    }


}
