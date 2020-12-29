using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Xunit;

namespace Boilerplate.Store.Tests.Utils
{
    [CollectionDefinition("Store Tests Collection")]
    public class StoreTestsCollection : ICollectionFixture<StoreFixture>
    {
    }

    /// <summary>
    /// This fixture works by creating and initializing a template database,
    /// then providing a new database, copied from the template one, to each
    /// test case.
    /// </summary>
    public class StoreFixture : IDisposable
    {
        readonly string _templateDbName;
        readonly StoreContext _context;

        public StoreFixture()
        {
            _templateDbName = $"template_db_{MakeId()}";

            _context = NewContext(_templateDbName);
            _context.Database.Migrate();

            InitializeMockData();

            _context.Database.CloseConnection();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Creates a new test database after the template database.
        /// </summary>
        public string CreateDatabase()
        {
            var dbName = $"test_db_{MakeId()}";

            using var conn = new NpgsqlConnection(NewConnectionString(_templateDbName));
            conn.Open();

            using var cmd = new NpgsqlCommand($"CREATE DATABASE {dbName} WITH TEMPLATE {_templateDbName}", conn);
            cmd.ExecuteNonQuery();

            return dbName;
        }

        public static StoreContext NewContext(string dbName)
        {
            return new StoreContext(NewContextOptions(dbName));
        }

        public static DbContextOptions<StoreContext> NewContextOptions(string dbName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseNpgsql(NewConnectionString(dbName));
            return optionsBuilder.Options;
        }

        public static string NewConnectionString(string dbName)
        {
            return $"Host=postgres;Database={dbName};Username=dbuser;Password=dbpassword";
        }

        /// <summary>
        /// Initialize mock data shared by all tests.
        /// </summary>
        void InitializeMockData()
        {
        }

        /// <summary>
        /// Generates a unique, monotonically increasing id for the test
        /// databases.
        /// </summary>
        static string MakeId()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        }
    }
}
