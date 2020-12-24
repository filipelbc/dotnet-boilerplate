using System;

namespace Boilerplate.Store.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public Person Author { get; set; } = null!;
        public Guid AuthorId { get; set; }
    }
}
