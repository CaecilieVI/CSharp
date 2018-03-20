using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Drawing;
using System.Linq;
using System.IO;

namespace BDSA2017.Assignment06.Tests
{
    public class ParallelOperationsTests
    {
        [Fact]
        public void Squares_given_1_and_5_returns_1_4_9_16_25()
        {
            var result = ParallelOperations.Squares(1, 5);
            var expected = new List<long> { 1, 4, 9, 16, 25 };

            Assert.Equal(expected, result.OrderBy(p => p));
        }

        [Fact]
        public void CreateThumbnails_resizer_called_with_right_parameters()
        {
            var strings = new string[] { "file000132701536" };
            var folder = "OutputFolder";
            var size = new Size(34,34);
            var mock = new Mock<IPictureModule>();

            ParallelOperations.CreateThumbnails(mock.Object, strings, folder, size);

            mock.Verify(s => s.Resize(strings[0], Path.Combine(folder, Path.GetFileName(strings[0])), size));
        }
    }
}
