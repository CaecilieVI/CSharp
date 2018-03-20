using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace BDSA2017.Assignment06
{
    public class ParallelOperations
    {
        public static ICollection<long> Squares(long lowerBound, long upperBound)
        {
            var list = new List<long>();

            Parallel.For(lowerBound, upperBound+1, i =>
            {
                list.Add(i * i);
            });

            return list;
        }

        public static void CreateThumbnails(IPictureModule resizer, IEnumerable<string> imageFiles, string outputFolder, Size size)
        {
            Parallel.ForEach(imageFiles, i =>
            {
                resizer.Resize(i, Path.Combine(outputFolder, Path.GetFileName(i)), size);
            });
        }
    }
}
