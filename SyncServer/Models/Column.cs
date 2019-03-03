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
        public Type DataType { get; set; }
    }
}
