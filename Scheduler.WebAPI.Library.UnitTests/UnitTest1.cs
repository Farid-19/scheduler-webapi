using System;
using Xunit;
using Scheduler.WebAPI.Library.DbContexts;
using Scheduler.WebAPI.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Scheduler.WebAPI.Library.UnitTests
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Retrieve_Users")]
        public void Test_Retrieve_Users()
        {
            using(var dbContext = GenerateDbContext())
            {
                dbContext.User.Add(GenerateTest_User());
                dbContext.SaveChanges();

                var users = dbContext.User.ToListAsync().Result;

                Assert.NotNull(users);
                Assert.NotEmpty(users);
            }
        }

        [Fact(DisplayName = "Retrieve_Events")]
        public void Test_Retrieve_Events()
        {
            using (var dbContext = GenerateDbContext())
            {
                dbContext.User.Add(GenerateTest_User());
                dbContext.SaveChanges();

                var users = dbContext.User.ToListAsync().Result;

                Assert.NotNull(users);
                Assert.NotEmpty(users);
            }
        }

        private User GenerateTest_User()
        {
            return new User()
            {
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Guid = Guid.NewGuid().ToString(),
                FirstName = "Sid",
                LastName = "Phillips",
                Email = "sidphillips@test.com",
            };
        }

        private Event GenerateTest_Event()
        {
            return new Event()
            {
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Guid = Guid.NewGuid().ToString(),
                Name = "Test event",
                Location = "Sid's House",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
            };
        }

        private SchedulerDbContext GenerateDbContext()
        {
            var builder = new DbContextOptionsBuilder<SchedulerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new SchedulerDbContext(builder.Options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
