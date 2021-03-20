using System;
using System.Linq;
using System.Threading.Tasks;
using TestedApplication.Database;
using Xunit;

namespace xUnitTests.Database
{
    public class TasksRepositoryTests : IDisposable
    {
        private readonly TasksRepositoryFixture _tasksRepositoryFixture;

        public TasksRepositoryTests()
        {
            this._tasksRepositoryFixture = new TasksRepositoryFixture();
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("")]
        [InlineData(null)]
        public async Task AddTest(string itemText)
        {
            //ARRANGE
            var repository = _tasksRepositoryFixture.Repository;

            //ACT
            await repository.AddTaskAsync(itemText);

            //ASSERT
            UserTask itemFromDb;
            using (var dbContext = _tasksRepositoryFixture.CreateDbContext())
            {
                itemFromDb = dbContext.UserTasks.SingleOrDefault(t => t.Name == itemText);
            }

            Assert.NotNull(itemFromDb);
        }

        [Fact]
        public async Task GetTasksNoItemsTest()
        {
            //ARRANGE
            var repository = _tasksRepositoryFixture.Repository;

            //ACT
            var tasks = await repository.GetTasksAsync();

            //ASSERT
            Assert.Empty(tasks);
        }
        [Fact]
        public async Task GetTasksSingleItemTest()
        {
            //ARRANGE
            await using var dbContext = _tasksRepositoryFixture.CreateDbContext();
            await dbContext.UserTasks.AddAsync(new UserTask() { Name = "TEST" });
            await dbContext.SaveChangesAsync();

            var repository = _tasksRepositoryFixture.Repository;

            //ACT
            var tasks = await repository.GetTasksAsync();

            //ASSERT
            Assert.Single(tasks);
        }

        [Fact]
        public async Task GetTasksMultipleItemsTest()
        {
            //ARRANGE
            var expectedTaskCount = 2;

            await using (var dbContext = _tasksRepositoryFixture.CreateDbContext())
            {
                for (int i = 0; i < expectedTaskCount; i++)
                {
                    await dbContext.UserTasks.AddAsync(new UserTask() { Name = "TEST" });
                }
                await dbContext.SaveChangesAsync();
            };

            var repository = _tasksRepositoryFixture.Repository;

            //ACT
            var tasks = await repository.GetTasksAsync();

            //ASSERT
            var actualTaskCount = tasks.Count();
            Assert.True(expectedTaskCount == actualTaskCount, $"Expected {expectedTaskCount} tasks, got {actualTaskCount}");
        }


        public void Dispose()
        {
            _tasksRepositoryFixture.Dispose();
        }
    }
}