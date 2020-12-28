using Xunit;

using Boilerplate.Store.Tests.Utils;
using Boilerplate.Store.Entities;

namespace Boilerplate.Store.Tests
{
    [Collection("Store Tests Collection")]
    public class StoreTests : BaseStoreTest
    {
        public StoreTests(StoreFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Test_Can_create_book_with_author()
        {
            _context.Add(new Book
            {
                Title = "How to code",
                Author = new Person
                {
                    Name = "Foo Bar"
                },
            });
            _context.SaveChanges();

            var ctx = NewContext();
            Assert.NotEmpty(ctx.Books);
            Assert.NotEmpty(ctx.Persons);
        }
    }
}
