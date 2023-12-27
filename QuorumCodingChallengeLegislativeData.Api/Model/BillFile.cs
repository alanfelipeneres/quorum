using QuorumCodingChallengeLegislativeData.Api.Model.Interfaces;
using System.Data;
using System.Globalization;

namespace QuorumCodingChallengeLegislativeData.Api.Model
{
    public class BillFile : IFile
    {
        private readonly IFormFile _file;

        public BillFile()
        {

        }

        public BillFile(IFormFile file)
        {
            _file = file;
        }

        public void ValidateFile()
        {
            using (var reader = new StreamReader(_file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length != 3)
                        throw new Exception("Bills File - Invalid content");
                }
            }
        }

        public IEnumerable<IEnumerable<string>> GetFileContent()
        {
            List<List<string>> content = new List<List<string>>();

            using (var reader = new StreamReader(_file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var lines = new List<string>();
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    foreach (var value in values)
                        lines.Add(value);

                    content.Add(lines);
                }
            }

            return content;
        }

        public DataTable GetDataTable(IEnumerable<IEnumerable<string>> rows, DataSet dataSet)
        {
            DataTable dataTable = dataSet.Tables.Add("bills");

            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("title", typeof(string));
            dataTable.Columns.Add("sponsor_id", typeof(int));

            DataRow dataRow;
            foreach (var line in rows)
            {
                if (rows.ToList().IndexOf(line) > 0)
                {
                    dataRow = dataTable.NewRow();
                    dataRow["id"] = line.ElementAt(0);
                    dataRow["title"] = line.ElementAt(1);
                    dataRow["sponsor_id"] = line.ElementAt(2);
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
    }
}
