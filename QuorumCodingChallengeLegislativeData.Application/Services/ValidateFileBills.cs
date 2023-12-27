using QuorumCodingChallengeLegislativeData.Application.Model;
using QuorumCodingChallengeLegislativeData.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuorumCodingChallengeLegislativeData.Application.Services
{
    public class ValidateFileBills : IValidateFile
    {
        public bool ValidateFile(MemoryStream file)
        {
            return true;
        }
    }
}
