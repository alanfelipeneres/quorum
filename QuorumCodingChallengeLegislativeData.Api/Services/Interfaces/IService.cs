using QuorumCodingChallengeLegislativeData.Api.Model.Interfaces;

namespace QuorumCodingChallengeLegislativeData.Api.Services.Interfaces
{
    public interface IService
    {
        FileStream CreateCsvResult(IEnumerable<IFile> files);
    }
}
