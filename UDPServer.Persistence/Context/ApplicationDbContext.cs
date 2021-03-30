using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPServer.Persistence.Models;

namespace UDPServer.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private const string ConnectionStringPath = "ApplicationDb";

        public DbSet<Message> Messages { get; set; }
        public DbSet<Sender> Senders { get; set; }

        public ApplicationDbContext() : base(ConnectionStringPath)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ApplicationDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
