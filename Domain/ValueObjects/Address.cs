using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Address : ValueObject
    {
        private Address()
        {
        
        }

        public static Address For(string addressString)
        {
            var address = new Address();

            try
            {
                var parts = addressString.Split(',');

                address.Country = parts[0];
                address.State = parts[1];
                address.City = parts[2];
                address.Street = parts[3];
            }
            catch (Exception ex)
            {
                throw new Exception(ValueObjectExceptions.AddAddressExcept(addressString), ex);
            }

            return address;
        }

        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }

        public static implicit operator string(Address address)
        {
            return address.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Country;
            yield return State;
            yield return City;
            yield return Street;
        }
    }
}
