using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Application.DTOs
{
    public class DepartamentoDTO
    {
        public string Departamento { get; set; }
        public string MesVigencia { get; set; }
        public int AnoVigencia { get; set; }
        public double TotalPagar { get; set; }
        public double TotalDesconto { get; set; }
        public double TotalExtras { get; set; }
        public List<FuncionarioDTO> Funcionarios{ get; set; }
    }
}
