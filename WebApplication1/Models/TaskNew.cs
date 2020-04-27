using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TaskNew
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int IdTeam { get; set; }
        public int IdAssignedTo { get; set; }
        public int IdCreator { get; set; }
        public string TaskType { get; set; }
    }
}
