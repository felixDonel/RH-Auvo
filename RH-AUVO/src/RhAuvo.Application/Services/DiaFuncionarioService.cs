using Dasync.Collections;
using RhAuvo.Application.DTOs;
using RhAuvo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Application.Services
{
    public class DiaFuncionarioService
    {
        public DiaFuncionarioService()
        {

        }

        public async Task<DiaFuncionario> CriarDiaFuncionario(DiaFuncionarioDTO diaDTO) {
            string[] horasintervalo = diaDTO.HoraIntervalo.Split("- ");
            var dia = DateTime.Parse(diaDTO.DataDia);
            var entrada = DateTime.Parse(diaDTO.DataDia + " " + diaDTO.HoraEntrada);
            var entradaDoIntervalo = DateTime.Parse(diaDTO.DataDia + " " + horasintervalo[0]);
            var saidaDoIntervalo = DateTime.Parse(diaDTO.DataDia + " " + horasintervalo[1]);
            var saida = DateTime.Parse(diaDTO.DataDia + " " + diaDTO.HoraSaida);

            TimeSpan horasAntesIntervalo = entradaDoIntervalo.Subtract(entrada);

            TimeSpan horasDepoisIntervalo = saida.Subtract(saidaDoIntervalo);
            var totalHoras = horasAntesIntervalo.TotalHours + horasDepoisIntervalo.TotalHours;
            ;
            return new DiaFuncionario() { dia = dia, HoraTrabalhada = totalHoras, ValorHora = diaDTO.ValorHora };
        }

        public async Task<List<DiaFuncionario>> CriarDiasFuncionario(List<DiaFuncionarioDTO> diasDTO) {
            List<DiaFuncionario> diasFuncionarios = new List<DiaFuncionario>();
            await diasDTO.ParallelForEachAsync( async dias => {
                diasFuncionarios.Add(await CriarDiaFuncionario(dias));
            },maxDegreeOfParallelism: 4);
            return diasFuncionarios;
        }
            
        
    }
}
