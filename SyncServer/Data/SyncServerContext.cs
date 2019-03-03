using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SyncServer.Models
{
    public class SyncServerContext : DbContext
    {
        public SyncServerContext (DbContextOptions<SyncServerContext> options)
            : base(options)
        {
        }

        public DbSet<SyncServer.Models.ProjectTableChangeSet> ProjectTableChangeSet { get; set; }
    }
}
