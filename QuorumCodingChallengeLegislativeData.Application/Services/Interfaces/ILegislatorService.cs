using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuorumCodingChallengeLegislativeData.Application.Services.Interfaces
{
    public interface ILegislatorService
    {
        string CreateCsvSupportOpposeCount(IEnumerable<MemoryStream> files);
    }
}
