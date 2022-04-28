using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string PublishingHouse { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
