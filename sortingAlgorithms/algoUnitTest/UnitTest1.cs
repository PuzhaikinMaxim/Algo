using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace algoUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private static Random random = new Random();

        [TestMethod]
        private static void selectionSortTest()
        {
            int[] array = generateArray(1000);
            selectionSort(array);
            Assert.IsTrue(true);
        }

        [TestMethod]
        private static void bubbleSortTest()
        {
            int[] array = generateArray(1000);
            bubbleSort(array);
            Assert.IsTrue(true);
        }

        [TestMethod]
        private static void insertionSortTest()
        {
            int[] array = generateArray(1000);
            insertionSort(array);
            Assert.IsTrue(true);
        }

        [TestMethod]
        private static void mergeSortTest()
        {
            int[] array = generateArray(1000);
            mergeSort(array);
            Assert.IsTrue(true);
        }

        private static int[] generateArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < array.Length; i++)
            {
                int randomNumber = random.Next(int.MaxValue / 2 - 1);
                array[i] = randomNumber;
            }
            return array;
        }

        private static void selectionSort(int[] array)
        {
            int minVal = int.MaxValue;
            int minValPos = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i; j < array.Length; j++)
                {
                    if (array[j] < minVal)
                    {
                        minVal = array[j];
                        minValPos = j;
                    }
                }
                int val = array[i];
                array[i] = minVal;
                array[minValPos] = val;
                minVal = int.MaxValue;
            }
        }

        private static void bubbleSort(int[] array)
        {
            bool isSorted = true;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int val = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = val;
                        isSorted = false;
                    }
                }
                if (isSorted)
                    break;
                isSorted = true;
            }
        }

        private static void insertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int sel = array[i];
                int j = i - 1;
                while (j >= 0 && sel < array[j])
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = sel;
            }
        }

        private static void mergeSort(int[] array)
        {
            mergeSort(array, 0, array.Length - 1);
        }

        private static void mergeSort(int[] array, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                int middleIndex = (startIndex + endIndex) / 2;
                mergeSort(array, startIndex, middleIndex);
                mergeSort(array, middleIndex + 1, endIndex);
                merge(array, startIndex, middleIndex, endIndex);
            }
        }

        private static void merge(int[] array, int startIndex, int middleIndex, int endIndex)
        {
            int leftPointer = startIndex;
            int rigthPointer = middleIndex + 1;
            int[] temporaryArray = new int[endIndex - startIndex + 1];
            int index = 0;

            while ((leftPointer <= middleIndex) && (rigthPointer <= endIndex))
            {
                if (array[leftPointer] < array[rigthPointer])
                {
                    temporaryArray[index] = array[leftPointer];
                    leftPointer++;
                }
                else
                {
                    temporaryArray[index] = array[rigthPointer];
                    rigthPointer++;
                }
                index++;
            }

            for (int i = leftPointer; i <= middleIndex; i++)
            {
                temporaryArray[index] = array[i];
                index++;
            }

            for (int i = rigthPointer; i <= endIndex; i++)
            {
                temporaryArray[index] = array[i];
                index++;
            }

            for (int i = 0; i < temporaryArray.Length; i++)
            {
                array[startIndex + i] = temporaryArray[i];
            }
        }
    }
}
