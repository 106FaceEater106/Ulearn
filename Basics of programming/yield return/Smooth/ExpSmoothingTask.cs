using System.Collections.Generic;
using System.Linq;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            double? prev = null;
            foreach (var item in data)
            {
                if (prev == null)
                {
                    prev = item.OriginalY;
                }
                item.ExpSmoothedY = (double)(alpha * item.OriginalY + (1 - alpha) * prev);
                prev = item.ExpSmoothedY;
                yield return item;
            }
        }
    }
}