using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.Dtos;
using TaskManager.Entities;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class TaskManagementNewController : Controller
    {
        private readonly ILogger<TaskManagementNewController> _logger;
        private ITaskRepository _taskRepository;

        public TaskManagementNewController(ITaskRepository customerRepository, ILogger<TaskManagementNewController> logger)
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
    }
}


