using QuorumCodingChallengeLegislativeData.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuorumCodingChallengeLegislativeData.Application.Services
{
    public class LegislatorService : ILegislatorService
    {
        private readonly IValidateFile _validateFile;

        public LegislatorService(IValidateFile validateFile)
        {
            _validateFile = validateFile;
        }

        public string CreateCsvSupportOpposeCount(IEnumerable<MemoryStream> files)
        {
            foreach (var file in files)
            {
                if(file != null)
                {
                    _validateFile.ValidateFile(file);
                }
            }

            return "";
        }
    }
}
