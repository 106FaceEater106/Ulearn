using System.Collections.Generic;
using System.Linq;


namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var numbersInWindow = new Queue<double>();
            var sum = 0.0;

            foreach (var item in data)
            {
                numbersInWindow.Enqueue(item.OriginalY);
                sum += item.OriginalY;
                item.AvgSmoothedY = sum / numbersInWindow.Count;

                if (numbersInWindow.Count == windowWidth)
                {
                    sum -= numbersInWindow.First();
                    numbersInWindow.Dequeue();
                }

                yield return item;
            }
        }
    }
}