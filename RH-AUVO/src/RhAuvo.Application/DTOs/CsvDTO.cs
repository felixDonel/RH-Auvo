using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Application.DTOs
{
    public class CsvDTO
    {
        public string Nome { get; set; }
        public List<string[]> TodoConteudo { get; set; }
        public List<LinhaCsvDTO> Linhas { get; set; }
    }
}
