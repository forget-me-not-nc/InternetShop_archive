using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public static class Settings
    {
        public static string GetDateFormat() => "dd/MM/yyyy HH:mm:ss";
        public static CultureInfo GetDateProvider() => CultureInfo.InvariantCulture;
    }
}
