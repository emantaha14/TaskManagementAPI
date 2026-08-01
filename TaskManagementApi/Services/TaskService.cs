using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services
{
    public class TaskService
    {
        private readonly AppDbContext _dbContext;
        public TaskService(AppDbContext dbContenct)
        {
            _dbContext = dbContenct;
        }
        public async Task<List<TaskItem>> GetAll()
        {
            return  await _dbContext.Tasks.AsNoTracking().ToListAsync();
        }

        public async Task<TaskItem?> GetById(int id)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        }

        public async Task<TaskItem> Add(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                IsCompleted = dto.IsCompleted,
            };

            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<bool> Update(int id, UpdateTaskDto dto) 
        {
            var task = await GetById(id);
            if (task == null) return false;

                task.Title = dto.Title;
                task.IsCompleted = dto.IsCompleted;
            await _dbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> Delete(int id) 
        {
            var task = await GetById(id);
            if (task == null) return false;
            else
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
