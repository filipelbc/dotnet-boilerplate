using System;

namespace Boilerplate.Store.Tests.Utils
{
    public class BaseStoreTest : IDisposable
    {
        protected readonly StoreContext _context;

        readonly string _dbName;

        public BaseStoreTest(StoreFixture fixture)
        {
            _dbName = fixture.CreateDatabase();
            _context = NewContext();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        public StoreContext NewContext()
        {
            return StoreFixture.NewContext(_dbName);
        }
    }
}
