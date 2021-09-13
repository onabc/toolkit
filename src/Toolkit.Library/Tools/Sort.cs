using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Library.Tools
{
    /// <summary>
    /// 排序
    /// </summary>
    public static class Sort
    {
        /// <summary>
        /// 冒泡
        /// </summary>
        /// <param name="array"></param>
        public static void BubbleSort(this int[] array)
        {
            if (array.IsBlankOrSingleElement()) return;

            bool flag = false;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        flag = true;

                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }

                if (!flag)
                {
                    break;
                }
                else
                {
                    flag = false;
                }
            }
        }

        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="array"></param>
        public static void SelectSort(this int[] array)
        {
            if (array.IsBlankOrSingleElement()) return;

            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[minIndex] > array[j])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    (array[i], array[minIndex]) = (array[minIndex], array[i]);
                }
            }
        }

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="array"></param>
        public static void InsertSort(this int[] array)
        {
            if (array.IsBlankOrSingleElement()) return;

            for (int i = 1; i < array.Length; i++)
            {
                int insertVal = array[i];
                int insertIndex = i - 1;
                while (insertIndex > 0 && insertVal < array[insertIndex])
                {
                    array[insertIndex + 1] = array[insertIndex];
                    insertIndex--;
                }
                array[insertIndex] = insertVal;
            }
        }

        /// <summary>
        /// 希尔排序-移动法
        /// </summary>
        /// <param name="array"></param>
        public static void ShellSort(this int[] array)
        {
            for (int gap = array.Length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < array.Length; i++)
                {
                    int index = i;
                    int temp = array[i];
                    while (index - gap >= 0 && temp < array[index - gap])
                    {
                        array[index] = array[index - gap];
                        index -= gap;
                    }
                    array[index] = temp;
                }
            }
        }

        /// <summary>
        /// 希尔排序-交换法
        /// </summary>
        /// <param name="array"></param>
        public static void ShellSortBySwap(this int[] array)
        {
            for (int gap = array.Length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < array.Length; i++)
                {
                    for (int j = i - gap; j >= 0; j -= gap)
                    {
                        if (array[j] > array[j + gap])
                        {
                            (array[j], array[j + gap]) = (array[j + gap], array[j]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="array"></param>
        public static void QuickSort(this int[] array)
        {
            Sort(array, 0, array.Length - 1);

            static void Sort(int[] array, int left, int right)
            {
                if (left > right) return;

                int pivot = array[left];
                int i = left, j = right;

                while (i != j)
                {
                    while (array[j] >= pivot && i < j)
                    {
                        j--;
                    }

                    while (array[i] <= pivot && i < j)
                    {
                        i++;
                    }

                    if (i < j)
                    {
                        int tmp = array[i];
                        array[i] = array[j];
                        array[j] = tmp;
                    }
                }
                // 将基准数放到中间的位置（基准数归位）
                array[left] = array[i];
                array[i] = pivot;

                // 递归，继续向基准的左右两边执行和上面同样的操作
                // i的索引处为上面已确定好的基准值的位置，无需再处理
                Sort(array, left, i - 1);
                Sort(array, i + 1, right);
            }
        }

        /// <summary>
        /// 数组为null或只有一个元素
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static bool IsBlankOrSingleElement(this int[] array)
        {
            return array is null || array.Length < 2;
        }
    }
}