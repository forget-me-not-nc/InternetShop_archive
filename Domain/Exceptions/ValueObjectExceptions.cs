using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValueObjectExceptions
    {
        public static string AddAddressExcept(string address) => "Invalid Address sequence. " + address;
    }
}
