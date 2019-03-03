using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SyncServer.Models
{
    public class ProjectTable
    {
        public ProjectTable()
        {

        }

        public ProjectTable(string name, IEnumerable<Column> columns)
        {
            Name = name;
            JsonDict = columns.ToDictionary(c => c.Name);
        }

        [Key]
        public string Name { get; set; }
        public bool SyncStatus { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public int RowVersion { get; set; } = 0;
        public string Json { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Dictionary<string, Column> JsonDict
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, Column>>(Json);
            }
            set
            {
                Json = JsonConvert.SerializeObject(value);
            }
        }
    }
}
