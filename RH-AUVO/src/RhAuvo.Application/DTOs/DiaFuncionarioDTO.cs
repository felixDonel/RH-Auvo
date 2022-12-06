using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Application.DTOs
{
    public class DiaFuncionarioDTO
    {
        public string DataDia { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSaida { get; set; }
        public string HoraIntervalo { get; set; }
        public double ValorHora { get; set; }
        public DiaFuncionarioDTO()
        {

        }
        public DiaFuncionarioDTO(string data, string entrada, string saida, string almoco, double valorHora)
        {
            DataDia = data;
            HoraEntrada = entrada;
            HoraSaida = saida;
            HoraIntervalo = almoco;
            ValorHora = valorHora;
        }
    }
}
