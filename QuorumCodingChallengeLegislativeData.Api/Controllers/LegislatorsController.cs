using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuorumCodingChallengeLegislativeData.Api.Model;
using QuorumCodingChallengeLegislativeData.Api.Model.Interfaces;
using QuorumCodingChallengeLegislativeData.Api.Model.Request;
using QuorumCodingChallengeLegislativeData.Api.Services;
using QuorumCodingChallengeLegislativeData.Api.Services.Interfaces;
//using QuorumCodingChallengeLegislativeData.Application.Model;
//using QuorumCodingChallengeLegislativeData.Application.Services.Interfaces;

namespace QuorumCodingChallengeLegislativeData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegislatorsController : ControllerBase
    {
        private readonly IService _service;

        public LegislatorsController(LegistorsService legislatorsService)
        {
            _service = legislatorsService;
        }

        [HttpPost("SupportOpposeCount")]
        public async Task<IActionResult>? SupportOpposeCount([FromForm] FilesRequest request)
        {
            try
            {
                if (request == null ||
                    request.BillsCSV.Length == 0 ||
                    request.LegislatorsCSV.Length == 0 ||
                    request.VotesCSV.Length == 0 ||
                    request.VoteResultCSV.Length == 0)
                {
                    return BadRequest("All files are required");
                }

                var files = new List<IFile>
                {
                    new BillFile(request.BillsCSV),
                    new LegislatorFile(request.LegislatorsCSV),
                    new VoteFile(request.VotesCSV),
                    new VoteResultFile(request.VoteResultCSV)
                };

                var responseFile = _service.CreateCsvResult(files);


                return File(responseFile, "text/csv", "legislators-support-oppose-count.csv");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
