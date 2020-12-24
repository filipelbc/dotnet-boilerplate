using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Boilerplate.Store;
using Boilerplate.Store.Entities;

namespace Boilerplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        readonly ILogger<BooksController> _logger;
        readonly StoreContext _context;

        public BooksController(ILogger<BooksController> logger, StoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _context.Books.ToArray();
        }
    }
}
