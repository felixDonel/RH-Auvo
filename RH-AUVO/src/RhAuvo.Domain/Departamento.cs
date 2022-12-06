using System;
using System.Collections.Generic;
using System.Linq;

namespace RhAuvo.Domain
{
    public class Departamento : Entity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MesVigente { get; set; }
        public int AnoVigente { get; set; }
        public double TotalPagar { get; private set; }
        public double TotalDescontos { get; set; }
        public double TotalExtra { get; set; }
        public List<Guid> FuncionarioId { get; set; }
        public virtual List<Funcionario> Funcionarios { get; set; }

        public void CalcularTotalPagar() {
            Funcionarios.ForEach(f => TotalPagar += f.TotalAReceber);
        }
        public void CalcularTotalDescontos()
        {
            Funcionarios.ForEach(f => TotalDescontos += f.ValorDescontos);
        }
        public void CalcularTotalExtra()
        {
            Funcionarios.ForEach(f => TotalExtra += f.ValorExtra);
        }

    }

}
