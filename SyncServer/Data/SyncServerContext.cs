using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using SyncServer.Models;

namespace SyncServer.Models
{
    public class SyncServerContext : DbContext
    {
        public SyncServerContext (DbContextOptions<SyncServerContext> options)
            : base(options)
        {
        }


        public DbSet<ProjectTable> ProjectTable { get; set; }
        public DbSet<SchemaDefinition> SchemaDefinitions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<DynamicEntity> DynamicEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SchemaDefinitionConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public class SchemaDefinitionConfiguration : IEntityTypeConfiguration<SchemaDefinition>
        {
            public void Configure(EntityTypeBuilder<SchemaDefinition> builder)
            {
                // This Converter will perform the conversion to and from Json to the desired type
                builder.Property(e => e.Columns).HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<Dictionary<string, Column>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }
        }

        public class DynamicEntityConfiguration : IEntityTypeConfiguration<DynamicEntity>
        {
            public void Configure(EntityTypeBuilder<DynamicEntity> builder)
            {
                // This Converter will perform the conversion to and from Json to the desired type
                builder.Property(e => e.Data).HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<Dictionary<string, object>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }
        }
    }
}
