using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;


namespace yield
{
    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var maxvaluesInWindow = new LinkedList<double>();
            var valuesInWindow = new LinkedList<double>();

            foreach (var item in data)
            {
                valuesInWindow.AddLast(item.OriginalY);

                while (maxvaluesInWindow.Count > 0 && maxvaluesInWindow.Last.Value < item.OriginalY)
                    maxvaluesInWindow.RemoveLast();

                maxvaluesInWindow.AddLast(item.OriginalY);

                if (valuesInWindow.Count > windowWidth)
                {
                    if (valuesInWindow.First.Value == maxvaluesInWindow.First.Value)
                        maxvaluesInWindow.RemoveFirst();
                    valuesInWindow.RemoveFirst();
                }


                item.MaxY = maxvaluesInWindow.First.Value;

                yield return item;
            }
        }
    }
}