using App.Entities;

namespace App.Repositories
{
    public interface IUserTasksRepository
    {
        Task<UserTask> GetByIdAsync(int taskId);
        Task<List<UserTask>> GetAllAsync();
        Task<List<UserTask>> GetAllByUserIdAsync(int userId);
        Task<UserTask> GetLastByUserIdAsync(int userId);
        Task<int> AddAsync(UserTask task);
        Task<bool> UpdateAsync(UserTask task);
        Task<bool> DeleteAsync(int taskId);
        Task<bool> DeleteLastByUserIdAsync(int userId);
    }
}