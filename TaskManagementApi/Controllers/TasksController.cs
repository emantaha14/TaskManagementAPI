using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;
using TaskManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
namespace TaskManagementApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {

            return  Ok(await _taskService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var task = await _taskService.Add(dto); 
            return CreatedAtAction(nameof(GetTaskById),
                new { id = task.Id }
                , task);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            var updated = await _taskService.Update(id, dto);
            if(!updated)return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.Delete(id);
            if(!deleted)return NotFound();
            else return NoContent();
        }
    }
}
