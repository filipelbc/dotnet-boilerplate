using System;

namespace Boilerplate.Store.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
