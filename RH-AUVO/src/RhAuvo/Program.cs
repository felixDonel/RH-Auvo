using RhAuvo.Application.Services;
using System;
using System.Threading.Tasks;

namespace RhAuvo
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();

        }

        static async Task MainAsync()
        {
            try
            {
                Console.WriteLine("Caminho da pasta: ");

                // Create a string variable and get user input from the keyboard and store it in the variable
                string Caminho = Console.ReadLine();
                string retorno = await new CsvService().FormatacaoJson(Caminho);
                //"C:\\Users\\Feeco\\source\\repos\\RH-Auvo\\RH-AUVO\\Arquivos"

                Console.WriteLine(retorno);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
