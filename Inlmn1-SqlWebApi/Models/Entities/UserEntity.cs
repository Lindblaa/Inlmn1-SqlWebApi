namespace Inlmn1_SqlWebApi.Models.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<IssueEntity> Issues { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }

    }
}
