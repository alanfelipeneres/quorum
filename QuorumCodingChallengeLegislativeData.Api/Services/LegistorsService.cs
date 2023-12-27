using QuorumCodingChallengeLegislativeData.Api.Model;
using QuorumCodingChallengeLegislativeData.Api.Model.Enum;
using QuorumCodingChallengeLegislativeData.Api.Model.Interfaces;
using QuorumCodingChallengeLegislativeData.Api.Services.Interfaces;
using System.Data;
using System.Globalization;

namespace QuorumCodingChallengeLegislativeData.Api.Services
{
    public class LegistorsService : IService
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
            List<string> lines = new List<string>
            {
                "id,name,num_supported_bills,num_opposed_bills"
            };

            //Preenchimento da variável a ser escrita no arquivo CSV
            foreach (DataRow row in dataSet.Tables["legislators"].Rows)
            {
                var num_supported_bills = dataSet.Tables["voteResult"].AsEnumerable().Count(
                                x => x.Field<int>("legislator_id") == (int)row["id"] &&
                                x.Field<int>("vote_type") == (int)EnumVoteType.Yes);

                var num_opposed_bills = dataSet.Tables["voteResult"].AsEnumerable().Count(
                                x => x.Field<int>("legislator_id") == (int)row["id"] &&
                                x.Field<int>("vote_type") == (int)EnumVoteType.No);

                lines.Add(string.Format("{0},{1},{2},{3}", 
                    row["id"], 
                    row["name"], 
                    num_supported_bills,
                    num_opposed_bills));
            }

            string path = @"d:\legislators-support-oppose-count.csv";
            if (File.Exists(path)) File.Delete(path);

            // Escrevendo os dados no arquivo CSV
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string linha in lines)
                    writer.WriteLine(linha);
            }

            if (!System.IO.File.Exists(path))
                throw new Exception("File not found");

            return File.OpenRead(path);
        }
    }
}
