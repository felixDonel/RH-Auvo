using System;

namespace RhAuvo.Domain
{
    public class DiaFuncionario : Entity
    {
        public Guid FuncionarioId { get; set; }
        public double ValorHora{ get; set; }
        public DateTime dia{ get; set; }
        public double HoraTrabalhada { get; set; }
        public int HorasAcordadas { get; set; } = 8;
    }


}
