using System.ComponentModel.DataAnnotations;

namespace Inlmn1_SqlWebApi.Models.Entities
{
    public class IssueEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public int StatusId { get; set; }
        [Required]
        public int UserId { get; set; }

        public UserEntity User { get; set; }
        public StatusEntity Status { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
    }
}
