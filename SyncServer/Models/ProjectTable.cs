using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncServer.Models
{
    public class ProjectTable
    {
        public ProjectTable(string name, string projectId)
        {
            Name = name;
            ProjectId = projectId;
        }

        [Key]
        public string Name { get; set; }
        public Project Project { get; set; }
        public string ProjectId { get; set; }
    }
}
