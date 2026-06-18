using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Entities;

namespace App.Repositories
{
    public class UserTasksRepository : IUserTasksRepository
    {
        private readonly AppDbContext dbContext;

        public UserTasksRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddAsync(UserTask task)
        {
            dbContext.UserTasks.Add(task);
            await dbContext.SaveChangesAsync();
            return task.Id;
        }

        public async Task<bool> DeleteAsync(int taskId)
        {
            dbContext.UserTasks.Remove(new UserTask { Id = taskId });
            int result = await dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLastByUserIdAsync(int userId)
        {
            var lastTask = await dbContext.UserTasks
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();

            if (lastTask == null)
            {
                return false;
            }

            dbContext.UserTasks.Remove(lastTask);
            int result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<UserTask>> GetAllAsync()
        {
            return await dbContext.UserTasks.ToListAsync();
        }

        public async Task<List<UserTask>> GetAllByUserIdAsync(int userId)
        {
            return await dbContext.UserTasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserTask> GetByIdAsync(int taskId)
        {
            return await dbContext.UserTasks
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<UserTask> GetLastByUserIdAsync(int userId)
        {
            return await dbContext.UserTasks
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(UserTask task)
        {
            dbContext.UserTasks.Update(task);
            int result = await dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}