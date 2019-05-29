using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        /// <summary>
        /// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
        /// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
        /// </summary>
        /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
        public static double Median(this IEnumerable<double> items)
        {
            var enumerable = items.OrderBy(x => x).ToList();
            var amount = enumerable.Count;
             
            if (amount == 0) throw new InvalidOperationException();

            var result = amount % 2 != 0
                ? enumerable[amount / 2]
                : (enumerable[amount / 2] + enumerable[amount / 2 - 1]) / 2.0;
            return result;
        }

        /// <returns>
        /// Возвращает последовательность, состоящую из пар соседних элементов.
        /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
        /// </returns>
        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            var isFirst = true;
            var prev = default(T);
            foreach (var item in items)
            {
                if (!isFirst) yield return new Tuple<T, T>(prev, item);
                prev = item;
                isFirst = false;
            }
        }
    }
}