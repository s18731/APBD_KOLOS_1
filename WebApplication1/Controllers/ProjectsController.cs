using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly Services.IProjectsDbService _dbService;

        public ProjectsController (IProjectsDbService service)
        {
            _dbService = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var QueryResult = _dbService.GetTaskInfo(id);

            if (QueryResult == null)
                return NotFound("Student with given Id does not exist.");
            else
                return Ok(QueryResult);
        }

        [HttpPost]
        public IActionResult InsertNewTask(TaskNew taskNew)
        {
;            if (!_dbService.InsertNewTask(taskNew))
                return BadRequest("Failed to insert");
            return Ok(taskNew);
        }
    }
}