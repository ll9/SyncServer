using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncServer.Models
{
    public class DynamicEntity
    {
        public DynamicEntity()
        {

        }

        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public int RowVersion { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public ProjectTable ProjectTable { get; set; }
        public string ProjectTableName { get; set; }
    }
}
