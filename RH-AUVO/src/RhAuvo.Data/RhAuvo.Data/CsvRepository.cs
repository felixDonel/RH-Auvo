using Dasync.Collections;
using RhAuvo.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhAuvo.Data
{
    public class CsvRepository
    {
        public async Task<List<Csv>> BuscarCsv(string caminho)
        {

            string[] arquivos = Directory.GetFiles(caminho, "*.csv", SearchOption.AllDirectories);



            return await LerPlanilhas(arquivos);
        }

        private async Task<List<Csv>> LerPlanilhas(string[] arquivos)
        {
            List<Csv> planilhas = new List<Csv>();

            await arquivos.ParallelForEachAsync(async a =>
            {
                using var file = new StreamReader($"{arquivos[0]}");
                List<string> linhas = new List<string>();
                while (!file.EndOfStream)
                {
                    linhas.Add(await file.ReadLineAsync());
                }

                string NomeDoArquivo = Path.GetFileNameWithoutExtension(a);
                file.Close();
                planilhas.Add(new Csv { Conteudo = linhas, Nome = NomeDoArquivo });
            }, maxDegreeOfParallelism: 4);
            return planilhas;
        }

    }
}
