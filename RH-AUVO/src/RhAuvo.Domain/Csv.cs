using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Domain
{
    public class Csv : Entity
    {
        public string Nome { get; set; }
        public List<string> Conteudo { get; set; }
    }
}
