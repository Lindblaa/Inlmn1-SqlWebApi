using Inlmn1_SqlWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inlmn1_SqlWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<StatusEntity> Statuses { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<IssueEntity> Issues { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }


    }
}
