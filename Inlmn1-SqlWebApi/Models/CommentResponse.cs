namespace Inlmn1_SqlWebApi.Models
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public DateTime Created { get; set; }
        public int UserId { get; set; }
    }
}