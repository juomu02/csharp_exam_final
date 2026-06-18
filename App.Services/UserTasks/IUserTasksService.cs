using App.Entities;

namespace App.Services
{
    public interface IUserTasksService
    {
        Task<IEnumerable<UserTask>> GetAllAsync();
        Task<IEnumerable<UserTask>> GetAllByUserIdAsync(int userId);
        Task<UserTask> GetLastByUserIdAsync(int userId);
        Task<UserTask> GetAsync(int taskId);
        Task<int> AddAsync(int userId, string title, string description,
            bool isCompleted, TaskImportance importance,
            DateTime? startDate, DateTime? endDate);
        Task<bool> UpdateAsync(int userId, int taskId, string title, string description,
            bool isCompleted, TaskImportance importance,
            DateTime? startDate, DateTime? endDate);
        Task<bool> UserDeleteAsync(int userId, int taskId);
        Task<bool> AdminDeleteAsync(int taskId);
        Task<bool> DeleteLastByUserIdAsync(int userId);
    }
}