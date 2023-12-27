using QuorumCodingChallengeLegislativeData.Api.Model.Enum;
using QuorumCodingChallengeLegislativeData.Api.Model.Interfaces;
using QuorumCodingChallengeLegislativeData.Api.Services.Interfaces;
using System.Data;

namespace QuorumCodingChallengeLegislativeData.Api.Services
{
    public class BillsService : IService
    {
        public FileStream CreateCsvResult(IEnumerable<IFile> files)
        {
            //Validando os arquivos
            foreach (var file in files)
                file.ValidateFile();

            //Criando o dataSet para facilitar a montagem do arquivo final
            var dataSet = new DataSet();
            foreach (var file in files)
                file.GetDataTable(file.GetFileContent(), dataSet);

            // Dados a serem escritos no arquivo CSV
            List<string> linhas = new List<string>
            {
                "id,title,supporter_count,opposer_count,primary_sponsor"
            };

            //Preenchimento da variável a ser escrita no arquivo CSV
            foreach (DataRow row in dataSet.Tables["voteResult"].Rows)
            {
                var supporter_count = dataSet.Tables["voteResult"].AsEnumerable()
                    .Count(x => x.Field<int>("vote_id") == (int)row["id"] && x.Field<int>("vote_type") == (int)EnumVoteType.Yes);

                var opposer_count = dataSet.Tables["voteResult"].AsEnumerable()
                    .Count(x => x.Field<int>("vote_id") == (int)row["id"] && x.Field<int>("vote_type") == (int)EnumVoteType.No);

                var primary_sponsor = dataSet.Tables["legislators"].AsEnumerable()
                    .Where(x => x.Field<int>("id") == (int)row["sponsor_id"])
                    .Select(x => x.Field<string>("name"))
                    .FirstOrDefault();

                linhas.Add(string.Format("{0},{1},{2},{3},{4}",
                    row["id"],
                    row["title"],
                    supporter_count,
                    opposer_count,
                    string.IsNullOrWhiteSpace(primary_sponsor) ? "Unknown": primary_sponsor));
            }

            string path = @"d:\bills.csv";
            if (File.Exists(path)) File.Delete(path);

            // Escrevendo os dados no arquivo CSV
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string linha in linhas)
                    writer.WriteLine(linha);
            }

            if (!System.IO.File.Exists(path))
                throw new Exception("File not found");

            return File.OpenRead(path);
        }
    }
}
