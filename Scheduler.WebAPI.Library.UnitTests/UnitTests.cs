using System;
using System.Linq;
using Xunit;
using Scheduler.WebAPI.Library.DbContexts;
using Scheduler.WebAPI.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Scheduler.WebAPI.Library.UnitTests
{
    public class UnitTests
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
                dbContext.Event.Add(GenerateTest_Event());
                dbContext.Event.Add(GenerateTest_Event());
                dbContext.SaveChanges();

                var events = dbContext.Event.ToListAsync().Result;

                Assert.NotNull(events);
                Assert.NotEmpty(events);
            }
        }

        [Fact(DisplayName = "Insert_User")]
        public void Test_Insert_User()
        {
            using (var dbContext = GenerateDbContext())
            {
                var guid = Guid.NewGuid().ToString();

                var user = GenerateTest_User();
                user.Guid = guid;

                dbContext.User.Add(user);
                dbContext.SaveChanges();

                var result = dbContext.User.First(u => u.Guid.Equals(guid));

                Assert.NotNull(result);
                Assert.Equal(guid, result.Guid);
            }
        }

        [Fact(DisplayName = "Insert_UserWithEvents")]
        public void Test_Insert_UserWithEvents()
        {
            using (var dbContext = GenerateDbContext())
            {
                var user = GenerateTest_User();
                user.Events.Add(GenerateTest_Event());
                user.Events.Add(GenerateTest_Event());
                user.Events.Add(GenerateTest_Event());

                dbContext.User.Add(user);
                dbContext.SaveChanges();

                var result = dbContext.User.First();

                Assert.NotNull(result);
                Assert.Equal(3, result.Events.Count);
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
