using QuorumCodingChallengeLegislativeData.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuorumCodingChallengeLegislativeData.Application.Model
{
    public class LegislatorsFile : MemoryStream, IValidateFile
    {
        public bool ValidateFile(MemoryStream file)
        {
            return false;
        }
    }
}
