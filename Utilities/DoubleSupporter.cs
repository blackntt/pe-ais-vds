using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS_New.Utilities
{
    public class DoubleSupporter
    {
        public static double parse_from_string(string value_str)
        {
            var style = System.Globalization.NumberStyles.AllowDecimalPoint;
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            double x_out;
            double.TryParse(value_str, style, culture, out x_out);
            //return double.Parse(value_str, style, culture);
            return x_out;
        }
    }
}
