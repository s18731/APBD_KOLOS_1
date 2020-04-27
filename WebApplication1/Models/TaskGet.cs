using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TaskGet
    {
        /* SELECT PROJECT.Name, TASK.IdTask, TASK.Name, TASK.Description, TASK.Deadline FROM TASK INNER JOIN PROJECT ON PROJECT.IdProject = TASK.IdProject WHERE TASK.IdProject = 1 ORDER BY Deadline; */
        public string ProjectName { get; set; }
        public int IdTask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
