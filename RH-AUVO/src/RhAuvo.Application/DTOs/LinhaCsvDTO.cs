using System.ComponentModel;

namespace RhAuvo.Application.DTOs
{
    public class LinhaCsvDTO
    {
        public int Linha { get; set; }
        [DisplayName("C�digo")]
        public int Codigo { get; set; }
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [DisplayName("Valor hora")]
        public double ValorHora { get; set; }
        [DisplayName("Data")]
        public string Data { get; set; }
        [DisplayName("Entrada")]
        public string Entrada { get; set; }
        [DisplayName("Sa�da")]
        public string Saida { get; set; }
        [DisplayName("Almo�o")]
        public string Almoco { get; set; }

        public LinhaCsvDTO()
        {

        }
        public LinhaCsvDTO(int codigo, string nome, double valorHora, string data, string entrada, string saida, string almoco)
        {
            Codigo = codigo;
            Nome = nome;
            ValorHora = valorHora;
            Data = data;
            Entrada = entrada;
            Saida = saida;
            Almoco = almoco;


        }
    }
}