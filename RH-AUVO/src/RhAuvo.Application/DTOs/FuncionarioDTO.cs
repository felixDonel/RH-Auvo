using System.Collections.Generic;

namespace RhAuvo.Application.DTOs
{
    public class FuncionarioDTO {

        public string Nome { get; set; }
        public int Codigo { get; set; }
        public double TotalReceber { get; set; }
        public double HoraExtras { get; set; }
        public double HorasDebito { get; set; }
        public int DiasFalta { get; set; }
        public int DiasExtras  { get; set; }
        public int DiasTrabalhados { get; set; }
    }
}
