using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Dtos;
using TaskManager.Entities;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class TaskManagementController : Controller
    {
        private readonly ILogger<TaskManagementController> _logger;
        private ITaskRepository _taskRepository;

        public TaskManagementController(ITaskRepository customerRepository, ILogger<TaskManagementController> logger)
        {
            _taskRepository = customerRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllTasks(TaskManagerQueryParameters queryParams)
        {
            var allTasks = _taskRepository.GetAll(queryParams).ToList();

            var allCustomersDto = allTasks.Select(x => Mapper.Map<TaskDto>(x));

            return Ok(allCustomersDto);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSingleTask")]
        public IActionResult GetSingleTask(long id)
        {          
            TaskEntity task = _taskRepository.GetSingle(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TaskDto>(task));
        }

        // POST api/customers

        [HttpPost]
        public IActionResult AddTask([FromBody] TaskCreateDto taskCreateDto)
        {
            if (taskCreateDto == null)
            {
                return BadRequest("DTO was null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = Mapper.Map<TaskEntity>(taskCreateDto);

            _taskRepository.Add(task);

            bool result = _taskRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            //return Ok(Mapper.Map<CustomerDto>(toAdd));
            return CreatedAtRoute("GetSingleTask", new { id = task.TaskId }, Mapper.Map<TaskDto>(task));
        }

        // PUT api/customers/{id}

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTask(long id, [FromBody] TaskUpdateDto updateDto)
        {
            var existingTask = _taskRepository.GetSingle(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            Mapper.Map(updateDto, existingTask);

            _taskRepository.Update(existingTask);

            bool result = _taskRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(Mapper.Map<TaskDto>(existingTask));
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult PartiallyUpdate(long id, [FromBody] JsonPatchDocument<TaskUpdateDto> taskPatchDoc)
        {
            if (taskPatchDoc == null)
            {
                return BadRequest();
            }

            var existingTask = _taskRepository.GetSingle(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            var taskToPatch = Mapper.Map<TaskUpdateDto>(existingTask);
            taskPatchDoc.ApplyTo(taskToPatch,ModelState);
            TryValidateModel(taskToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(taskToPatch, existingTask);

            _taskRepository.Update(existingTask);

            bool result = _taskRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(Mapper.Map<TaskUpdateDto>(existingTask));
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(long id)
        {
            var existingTask = _taskRepository.GetSingle(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            _taskRepository.Delete(id);

            bool result = _taskRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return NoContent();
        }

    }
}

