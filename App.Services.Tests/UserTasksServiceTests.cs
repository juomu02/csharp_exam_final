using Moq;
using App.Entities;
using App.Repositories;

namespace App.Services.Tests
{
    public class UserTasksServiceTests
    {
        private readonly Mock<IUserTasksRepository> userTaskRepositoryMock =
            new Mock<IUserTasksRepository>();
        private readonly UserTasksService userTasksService;

        public UserTasksServiceTests()
        {
            userTasksService = new UserTasksService(userTaskRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_ReturnsTaskId_WhenTaskIsAddedSuccessfully()
        {
            var userId = 50;
            var title = "Test task";
            var description = "Test description";
            var isCompleted = false;
            var importance = TaskImportance.Medium;
            var startDate = DateTime.UtcNow;
            var expectedTaskId = 100;

            userTaskRepositoryMock.Setup(dbContext => dbContext.AddAsync(It.IsAny<UserTask>()))
                .ReturnsAsync(expectedTaskId);

            var result = await userTasksService.AddAsync(
                    userId, title, description, isCompleted, importance, startDate, null);

            Assert.Equal(expectedTaskId, result);
            userTaskRepositoryMock.Verify(dbContext => dbContext.AddAsync(It.IsAny<UserTask>()), Times.Once);
        }

    }
}