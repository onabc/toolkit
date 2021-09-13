using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Library.Extensions;
using Toolkit.Library.Tools;

namespace Toolkit.Test
{
    [TestFixture]
    public class SortTest
    {
        private int[] array;
        private Stopwatch stopwatch;

        [SetUp]
        public void Initialize()
        {
            stopwatch = new Stopwatch();

            Random random = new Random();
            array = new int[10000];
            Parallel.For(0, array.Length, i => array[i] = random.Next(0, 23333));
        }

        [Test]
        public void BubbleSortTest()
        {
            array.BubbleSort();
            bool isOrderly = IsOrderlyArray(array);
            Assert.IsTrue(isOrderly);
        }

        [Test]
        public void ShellSortTest()
        {
            array.ShellSort();
            bool isOrderly = IsOrderlyArray(array);
            Assert.IsTrue(isOrderly);
        }

        private bool IsOrderlyArray(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                    return false;
            }
            return true;
        }
    }
}