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
        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<List<TaskItem>> GetAll()
        {
            return  await _taskRepository.GetAllAsync();
        }

        public async Task<TaskItem?> GetById(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<TaskItem> Add(CreateTaskDto dto)
        {
            var task = _mapper.Map<TaskItem>(dto);
            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveAsync();

            return task;
        }

        public async Task<bool> Update(int id, UpdateTaskDto dto) 
        {
            var task = await GetById(id);
            if (task == null) return false;
            _mapper.Map(dto, task);
             _taskRepository.Update(task);
            await _taskRepository.SaveAsync();
            
            return true;
        }

        public async Task<bool> Delete(int id) 
        {
            var task = await GetById(id);
            if (task == null) return false;
            else
            {
                 _taskRepository.Delete(task);
                await _taskRepository.SaveAsync();
                return true;
            }
        }
    }
}
