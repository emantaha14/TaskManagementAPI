using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories;

namespace TaskManagementApi.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;
        public TaskService(ITaskRepository taskRepository, IMapper mapper, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<TaskItem>> GetAll()
        {
            _logger.LogInformation("Getting all tasks.");
            return  await _taskRepository.GetAllAsync();
        }

        public async Task<TaskItem?> GetById(int id)
        {
            _logger.LogInformation("Getting task with id {TaskId}", id);
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<TaskItem> Add(CreateTaskDto dto)
        {
            _logger.LogInformation("Creating a new task.");
            var task = _mapper.Map<TaskItem>(dto);
            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveAsync();
            _logger.LogInformation("Task created successfully with id {TaskId}.", task.Id);
            return task;
        }

        public async Task<bool> Update(int id, UpdateTaskDto dto) 
        {
            _logger.LogInformation("Updating task with id {TaskId}.", id);
            var task = await GetById(id);
            if (task == null) {
                _logger.LogWarning("Cannot update. Task with id {TaskId} was not found.", id);
                return false;
            }
            
            _mapper.Map(dto, task);
             _taskRepository.Update(task);
            await _taskRepository.SaveAsync();
            _logger.LogInformation("Task with id {TaskId} updated successfully.", id);

            return true;
        }

        public async Task<bool> Delete(int id) 
        {
            _logger.LogInformation("Deleting task with id {TaskId}.", id);
            var task = await GetById(id);
            if (task == null) 
            {
                _logger.LogWarning("Cannot delete. Task with id {TaskId} was not found.", id);
                return false;
            }

           
            else
            {
                _taskRepository.Delete(task);
                await _taskRepository.SaveAsync();
                _logger.LogInformation("Task with id {TaskId} deleted successfully.", id);
                return true;
            }
        }
    }
}
