using System.Collections;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProjectsDbService
    {
        public List<TaskGet> GetTaskInfo (int projectId);
        public bool InsertNewTask(TaskNew taskNew);
        public bool DatabaseContainsTaskType(int id, string name);
    }
}