
using App.Repositories;
using App.Entities;

namespace App.Services
{
    public class UserTasksService : IUserTasksService
    {
        public readonly IUserTasksRepository userTasksRepository;
        public UserTasksService(IUserTasksRepository userTasksRepository)
        {
            this.userTasksRepository = userTasksRepository;
        }
        public async Task<int> AddAsync(int userId, string title, string description, bool isCompleted, TaskImportance importance, DateTime? startDate, DateTime? endDate)
        {
            var newTask = new UserTask
            {
                UserId = userId,
                Title = title,
                Description = description,
                IsCompleted = isCompleted,
                Importance = importance,
                StartDate = startDate,
                EndDate = endDate
            };

            return await userTasksRepository.AddAsync(newTask);
        }

        public async Task<bool> UserDeleteAsync(int userId, int taskId)
        {
            var task = await userTasksRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new ArgumentException("Task not found.");
            }
            if (task.UserId != userId)
            {
                throw new UnauthorizedAccessException("User does not have permission to delete this task.");
            }
            return await userTasksRepository.DeleteAsync(taskId);
        }
        public async Task<bool> AdminDeleteAsync(int taskId)
        {
            var task = await userTasksRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new ArgumentException("Task not found.");
            }
            return await userTasksRepository.DeleteAsync(taskId);
        }

        public async Task<bool> DeleteLastByUserIdAsync(int userId)
        {
            var lastTask = await userTasksRepository.GetLastByUserIdAsync(userId);
            if (lastTask == null)
            {
                throw new ArgumentException("No tasks found for the user.");
            }
            return await userTasksRepository.DeleteLastByUserIdAsync(userId);
        }

        public async Task<IEnumerable<UserTask>> GetAllAsync()
        {
            return await userTasksRepository.GetAllAsync();
        }

        public async Task<IEnumerable<UserTask>> GetAllByUserIdAsync(int userId)
        {
            return await userTasksRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<UserTask> GetAsync(int taskId)
        {
            var task = await userTasksRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new ArgumentException("Task not found.");
            }
            return task;
        }

        public async Task<UserTask> GetLastByUserIdAsync(int userId)
        {
            var lastTask = await userTasksRepository.GetLastByUserIdAsync(userId);
            if (lastTask == null)
            {
                throw new ArgumentException("No tasks found for the user.");
            }
            return lastTask;
        }

        public async Task<bool> UpdateAsync(int userId, int taskId, string title, string description, bool isCompleted, TaskImportance importance, DateTime? startDate, DateTime? endDate)
        {
            var task = await userTasksRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new ArgumentException("Task not found.");
            }
            if (task.UserId != userId)
            {
                throw new UnauthorizedAccessException("User does not have permission to update this task.");
            }

            task.Title = title;
            task.Description = description;
            task.IsCompleted = isCompleted;
            task.Importance = importance;
            task.StartDate = startDate;
            task.EndDate = endDate;

            return await userTasksRepository.UpdateAsync(task);
        }
    }
}