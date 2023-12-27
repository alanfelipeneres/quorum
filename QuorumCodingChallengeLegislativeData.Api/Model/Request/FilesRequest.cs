namespace QuorumCodingChallengeLegislativeData.Api.Model.Request
{
    public class FilesRequest
    {
        public IFormFile BillsCSV { get; set; }
        public IFormFile LegislatorsCSV { get; set; }
        public IFormFile VotesCSV { get; set; }
        public IFormFile VoteResultCSV { get; set; }
    }
}
