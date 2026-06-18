using Microsoft.AspNetCore.Mvc;
using App.Services;
using App.API.Models.Requests;
using Microsoft.AspNetCore.Authorization;

namespace App.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserTasksController : ControllerBase
    {
        private readonly IUserTasksService userTasksService;

        public UserTasksController(IUserTasksService userTasksService)
        {
            this.userTasksService = userTasksService;
        }

        [Authorize(Policy = "userOnly")]
        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            var result = await userTasksService.AddAsync(
                userId: int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value),
                title: request.Title,
                description: request.Description,
                isCompleted: request.IsCompleted,
                importance: request.Importance,
                startDate: request.StartDate,
                endDate: request.EndDate
            );

            return Ok(result);
        }

        [Authorize(Policy = "adminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await userTasksService.GetAllAsync();
            return Ok(tasks);
        }

        [Authorize(Policy = "userOnly")]
        [HttpGet("by-userid/{userId}")]
        public async Task<IActionResult> GetTasksByUserId(int userId)
        {
            var tasks = await userTasksService.GetAllByUserIdAsync(userId);
            return Ok(tasks);
        }

        [Authorize(Policy = "adminOnly")]
        [HttpGet("last/by-userid/{userId}")]
        public async Task<IActionResult> GetLastTaskByUserId(int userId)
        {
            var task = await userTasksService.GetLastByUserIdAsync(userId);
            return Ok(task);
        }

        [Authorize(Policy = "userOnly")]
        [HttpGet("my-tasks")]
        public async Task<IActionResult> GetMyTasks()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            var tasks = await userTasksService.GetAllByUserIdAsync(userId);
            return Ok(tasks);
        }

        [Authorize(Policy = "userOnly")]
        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UpdateTaskRequest request)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            var result = await userTasksService.UpdateAsync(userId, taskId, request.Title,
                 request.Description, request.IsCompleted, request.Importance, request.StartDate, request.EndDate);
            return Ok(result);
        }

        [Authorize(Policy = "adminOnly")]
        [HttpDelete("last/by-userid/{userId}")]
        public async Task<IActionResult> DeleteLastTaskByUserId(int userId)
        {
            var result = await userTasksService.DeleteLastByUserIdAsync(userId);
            return Ok(result);
        }

        [Authorize(Policy = "adminOnly")]
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTaskById(int taskId)
        {
            var result = await userTasksService.AdminDeleteAsync(taskId);
            return Ok(result);
        }

        [Authorize(Policy = "userOnly")]
        [HttpDelete("my-tasks/{taskId}")]
        public async Task<IActionResult> DeleteMyTask(int taskId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            var result = await userTasksService.UserDeleteAsync(userId, taskId);
            return Ok(result);
        }
    }
}