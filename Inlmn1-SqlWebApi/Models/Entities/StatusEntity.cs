namespace Inlmn1_SqlWebApi.Models.Entities
{
    public class StatusEntity
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public ICollection<IssueEntity> Issues { get; set; }

    }
}
