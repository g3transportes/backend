using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Helpers
{
    public class Comum
    {
        public static int CalculaTotalPages(int totalItems, int pageSize)
        {
            int result = (int)Math.Ceiling((decimal)totalItems / pageSize);

            return result;
        }

        public static IEnumerable<Tuple<int, int>> MonthsBetween(DateTime startDate, DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            while (iterator <= limit)
            {
                yield return Tuple.Create(iterator.Month, iterator.Year);
                iterator = iterator.AddMonths(1);
            }
        }

    }
}
