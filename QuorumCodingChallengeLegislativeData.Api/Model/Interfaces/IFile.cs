using System.Data;

namespace QuorumCodingChallengeLegislativeData.Api.Model.Interfaces
{
    public interface IFile
    {
        void ValidateFile();
        IEnumerable<IEnumerable<string>> GetFileContent();
        public DataTable GetDataTable(IEnumerable<IEnumerable<string>> rows, DataSet dataSet);
    }
}
