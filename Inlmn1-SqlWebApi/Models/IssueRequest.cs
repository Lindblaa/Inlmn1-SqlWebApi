namespace Inlmn1_SqlWebApi.Models
{
    public class IssueRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int UserId { get; set; }
    }
}
