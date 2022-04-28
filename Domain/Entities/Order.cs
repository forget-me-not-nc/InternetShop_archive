using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Order
    {
        public string OrderId { get; set; }

        public int ClientId { get; set; }

        public ICollection<Book> Books { get; set; }

        public decimal TotalPrice { get; set; }
        public Address Address { get; set; }
        public DateTime Date { get; set; }
    }
}
