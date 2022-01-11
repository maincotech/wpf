using Maincotech;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WpfTemplate.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<DbSettingInfo> DbSettingInfoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var s_migrationSqlitePath = Path.Combine(AppRuntimeContext.ExecutingPath, "data", "automation.sqlite3");
            var connectionString = new SqliteConnectionStringBuilder { DataSource = s_migrationSqlitePath }.ToString();
            optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
        }
    }
}