namespace Inlmn1_SqlWebApi.Models
{
    public class CommentRequest
    {
        public string Message { get; set; }
        public int IssueId { get; set; }
        public int UserId { get; set; }
    }
}
