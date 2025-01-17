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

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Benches
            modelBuilder.Entity<Bench>().HasData(
                new Bench { Id = 1, Name = "Bench A" },
                new Bench { Id = 2, Name = "Bench B" }
            );

            // Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Longitude = "40.712776", Latitude = "-74.005974" },
                new Location { Id = 2, Longitude = "34.052235", Latitude = "-118.243683" }
            );

            // Statuses
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Type = "Active" },
                new Status { Id = 2, Type = "Inactive" }
            );

            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "John", First_name = "Doe", Email = "john.doe@example.com", Password = "hashedpassword1" },
                new User { Id = 2, Name = "Jane", First_name = "Smith", Email = "jane.smith@example.com", Password = "hashedpassword2" }
            );

            // Conversations
            modelBuilder.Entity<Conversation>().HasData(
                new Conversation
                {
                    Id = 1,
                    start_datetime = new DateTime(2025, 01, 01, 10, 0, 0),
                    end_datetime = new DateTime(2025, 01, 01, 10, 30, 0),
                    sentiment = 1,
                    summary = "First conversation summary",
                    BenchId = 1
                },
                new Conversation
                {
                    Id = 2,
                    start_datetime = new DateTime(2025, 01, 02, 11, 0, 0),
                    end_datetime = new DateTime(2025, 01, 02, 11, 20, 0),
                    sentiment = 0,
                    summary = "Second conversation summary",
                    BenchId = 2
                }, 
                new Conversation
                {
                    Id = 3,
                    start_datetime = new DateTime(2025, 01, 02, 12, 0, 0),
                    end_datetime = new DateTime(2025, 01, 02, 12, 20, 0),
                    sentiment = 0,
                    summary = "Third conversation summary",
                    BenchId = 2
                }
            );

            // Questions
            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    Text = "What do you think about this neighbourhood?",
                    made_at = new DateTime(2025, 01, 01, 9, 0, 0),
                    is_active = true,
                    order_number = 1
                },
                new Question
                {
                    Id = 2,
                    Text = "What new features do you think this village need?",
                    made_at = new DateTime(2025, 01, 01, 9, 5, 0),
                    is_active = true,
                    order_number = 2
                },
                new Question
                {
                    Id = 3,
                    Text = "Do you think that our townhall is modern enough?",
                    made_at = new DateTime(2025, 01, 01, 9, 8, 0),
                    is_active = false,
                    order_number = 3
                },
                new Question
                {
                    Id = 4,
                    Text = "Does our local park need a new extention?",
                    made_at = new DateTime(2025, 01, 02, 9, 8, 0),
                    is_active = false,
                    order_number = 4
                },
                new Question
                {
                    Id = 5,
                    Text = "What do you think of our local museum?",
                    made_at = new DateTime(2025, 01, 03, 9, 8, 0),
                    is_active = false,
                    order_number = 5
                }
            );

            // Answers
            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    Id = 1,
                    Response = "It's nice and peaceful.",
                    ConversationId = 1,
                    QuestionId = 1
                },
                new Answer
                {
                    Id = 2,
                    Response = "Maybe more greens, vegetation would be nice.",
                    ConversationId = 1,
                    QuestionId = 2
                }
            );

            // Histories
            modelBuilder.Entity<History>().HasData(
                new History
                {
                    Id = 1,
                    BenchId = 1,
                    LocationId = 1,
                    StatusId = 1
                },
                new History
                {
                    Id = 2,
                    BenchId = 2,
                    LocationId = 2,
                    StatusId = 2
                }
            );
        }
    }
}
