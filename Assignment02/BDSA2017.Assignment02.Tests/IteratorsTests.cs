using System;
using System.Collections.Generic;
using Xunit;

namespace BDSA2017.Assignment02.Tests
{
    public class IteratorsTests
    {
        [Fact]
        public void Flatten_given_stream_of_stream_returns_stream()
        {
            int[] stream1 = { 1, 2, 3 };
            int[] stream2 = { 10, 20, 30 };
            int[] stream3 = { 64, 89, 200 };

            int[][] all = { stream1, stream2, stream3 };

            int[] expected = { 1, 2, 3, 10, 20, 30, 64, 89, 200 };
            var actual = Helpers.Flatten(all);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_given_stream_and_predicate_returns_stream_if_predicate_is_true()
        {
            int[] stream = { 1, 2, 3, 4, 5, 6, 7, 8 };

            int[] expected = { 1, 3, 5, 7 };
            Predicate<int> uneven = Uneven;
            var actual = Helpers.Filter(stream, uneven);

            Assert.Equal(expected, actual);
        }

        public static bool Uneven(int i)
        {
            return i % 2 != 0;
        }
    }
}
