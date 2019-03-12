using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncServer.Models
{
    public class SchemaDefinition
    {

        [Key]
        public string Id { get; set; }
        public Dictionary<string, Column> Columns { get; set; }

        public bool IsDeleted { get; set; } = false;
        public int RowVersion { get; set; } = 0;

        public string ProjectTableName { get; set; }
        public string ProjectId { get; set; }
    }
}
