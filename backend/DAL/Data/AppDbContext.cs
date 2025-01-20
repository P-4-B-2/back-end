using backend.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Bench> Benches { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.Bench)
                .WithMany(b => b.Conversations)
                .HasForeignKey(c => c.BenchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Answers)
                .WithOne(a => a.Conversation)
                .HasForeignKey(a => a.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<History>()
                .HasOne(h => h.Bench)
                .WithMany()
                .HasForeignKey(h => h.BenchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<History>()
                .HasOne(h => h.Location)
                .WithMany(l => l.Histories)
                .HasForeignKey(h => h.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<History>()
                .HasOne(h => h.Status)
                .WithMany(s => s.Histories)
                .HasForeignKey(h => h.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);

            SeedData.Apply(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
