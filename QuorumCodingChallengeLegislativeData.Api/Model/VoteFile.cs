using QuorumCodingChallengeLegislativeData.Api.Model.Interfaces;
using System.Data;

namespace QuorumCodingChallengeLegislativeData.Api.Model
{
    public class VoteFile : IFile
    {
        private readonly IFormFile _file;

        public VoteFile()
        {
            
        }

        public VoteFile(IFormFile file)
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

                    if (values.Length != 2)
                        throw new Exception("Votes File - Invalid content");
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

        //public DataSet GetDataSet(IEnumerable<IEnumerable<string>> rows)
        //{
        //    if (rows == null || rows.Count() == 0)
        //        throw new Exception("Vote File - Dataset wasn't created");

        //    DataSet dataSet = new DataSet();
        //    DataTable dataTable = dataSet.Tables.Add("voteFile");

        //    dataTable.Columns.Add("id", typeof(int));
        //    dataTable.Columns.Add("bill_id", typeof(int));

        //    DataRow dataRow;
        //    foreach (var line in rows)
        //    {
        //        if (rows.ToList().IndexOf(line) > 0)
        //        {
        //            dataRow = dataTable.NewRow();
        //            dataRow["id"] = line.ElementAt(0);
        //            dataRow["bill_id"] = line.ElementAt(1);
        //            dataTable.Rows.Add(dataRow);
        //        }
        //    }

        //    return dataSet;
        //}

        public DataTable GetDataTable(IEnumerable<IEnumerable<string>> rows, DataSet dataSet)
        {
            DataTable dataTable = dataSet.Tables.Add("voteFile");

            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("bill_id", typeof(int));

            DataRow dataRow;
            foreach (var line in rows)
            {
                if (rows.ToList().IndexOf(line) > 0)
                {
                    dataRow = dataTable.NewRow();
                    dataRow["id"] = line.ElementAt(0);
                    dataRow["bill_id"] = line.ElementAt(1);
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
    }
}
