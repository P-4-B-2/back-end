using backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace backend.DAL.Data
{
    public static class SeedData
    {
        public static void Apply(ModelBuilder modelBuilder)
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
                new Conversation { Id = 1, start_datetime = new DateTime(2025, 01, 01, 10, 0, 0), end_datetime = new DateTime(2025, 01, 01, 10, 30, 0), sentiment = 85, summary = "First conversation summary", BenchId = 1 },
                new Conversation { Id = 2, start_datetime = new DateTime(2025, 01, 02, 11, 0, 0), end_datetime = new DateTime(2025, 01, 02, 11, 20, 0), sentiment = 60, summary = "Second conversation summary", BenchId = 2 },
                new Conversation { Id = 3, start_datetime = new DateTime(2025, 01, 02, 12, 0, 0), end_datetime = new DateTime(2025, 01, 02, 12, 20, 0), sentiment = 40, summary = "Third conversation summary", BenchId = 2 },
                new Conversation { Id = 4, start_datetime = new DateTime(2025, 01, 03, 14, 0, 0), end_datetime = new DateTime(2025, 01, 03, 14, 45, 0), sentiment = 70, summary = "Fourth conversation summary", BenchId = 1 },
                new Conversation { Id = 5, start_datetime = new DateTime(2025, 01, 04, 15, 0, 0), end_datetime = new DateTime(2025, 01, 04, 15, 30, 0), sentiment = 30, summary = "Fifth conversation summary", BenchId = 2 },
                new Conversation { Id = 6, start_datetime = new DateTime(2025, 01, 05, 16, 0, 0), end_datetime = new DateTime(2025, 01, 05, 16, 20, 0), sentiment = 90, summary = "Sixth conversation summary", BenchId = 1 }
            );

            // Questions
            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, Text = "What do you think about this neighbourhood?", MadeAt = new DateTime(2025, 01, 01, 9, 0, 0), IsActive = true, OrderNumber = 1 },
                new Question { Id = 2, Text = "What new features do you think this village needs?", MadeAt = new DateTime(2025, 01, 01, 9, 5, 0), IsActive = true, OrderNumber = 2 }
            );

            // Answers
            modelBuilder.Entity<Answer>().HasData(
                new Answer { Id = 1, Response = "It's nice and peaceful.", ConversationId = 1, QuestionId = 1 },
                new Answer { Id = 2, Response = "Maybe more greens, vegetation would be nice.", ConversationId = 1, QuestionId = 2 }
            );

            // Histories
            modelBuilder.Entity<History>().HasData(
                new History { Id = 1, BenchId = 1, LocationId = 1, StatusId = 1 },
                new History { Id = 2, BenchId = 2, LocationId = 2, StatusId = 2 }
            );
        }
    }
}
