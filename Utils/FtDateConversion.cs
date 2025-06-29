using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Utils
{
    class FtDateConversion
    {

        public static string ConvertDateFormatTo(string inputDate, string fromFormat= "dddd, MMMM dd, yyyy", string formatTo= "yyyy/MM/dd")
        {
            DateTime date = DateTime.ParseExact(inputDate, fromFormat, CultureInfo.InvariantCulture);
            string formattedDate = date.ToString(formatTo);

            return formattedDate;
        }

    }
}
