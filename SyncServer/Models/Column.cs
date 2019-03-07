using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncServer.Models
{
    public class Column
    {
        public Column(string name, Type dataType)
        {
            Name = name;
            DataType = dataType;
        }

        public string Name { get; set; }
        [JsonIgnore]
        public Type DataType { get; set; }

        [JsonProperty(PropertyName = "RecordType")]
        private string DataTypeString => DataType.Name;
    }
}
