using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace sortingAlgorithms
{
    class Program
    {
        private delegate void sortArray(int[] array);
        private static Dictionary<int, sortArray> sortingAlgos;
        private static Random random = new Random();
        static void Main(string[] args)
        {
            sortingAlgos = new Dictionary<int, sortArray>()
            {
                {1, selectionSort},
                {2, bubbleSort},
                {3, insertionSort},
                {4, mergeSort}
            };
            int[] array;
            //while (true)
            //{
            //    Console.WriteLine("Введите расположение файла");
            //    string filePath = Console.ReadLine();
            //    try
            //    {
            //        string fileContent = readFile(filePath);
            //        array = tryParseToIntArray(fileContent);
            //    }
            //    catch (Exception)
            //    {
            //        continue;
            //    }

            //    Console.WriteLine("Ваш массив: ");
            //    printArray(array);

            //    Console.WriteLine("Выберите алгоритм сортировки");
            //    Console.WriteLine("1 - Сортировка выбором, 2 - Сортировка пузырьком, 3 - Сортировка вставками, 4 - Сортировка слиянием (сортировка по умолчанию: Сортировка выбором)");

            //    int sortingType;
            //    try
            //    {
            //        sortingType = Int32.Parse(Console.ReadLine());
            //    }
            //    catch (Exception)
            //    {
            //        continue;
            //    }

            //    if (sortingType > 4 || sortingType < 1)
            //        sortingType = 1;
            //    sortingAlgos[sortingType](array);

            //    Console.WriteLine("Ваш отсортированный массив: ");
            //    printArray(array);
            //    rewriteFile(array, filePath);
            //}
            //int[] array = generateArray(1000);
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //mergeSort(array);
            //sw.Stop();
            ////printArray(array);
            //Console.WriteLine(sw.ElapsedMilliseconds);
            algoTest(sortingAlgos[1]);
            algoTest(sortingAlgos[2]);
            algoTest(sortingAlgos[3]);
            algoTest(sortingAlgos[4]);
        }

        private static void selectionSort(int[] array)
        {
            int minVal = int.MaxValue;
            int minValPos=0;
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = i; j < array.Length; j++)
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

        private static string readFile(string path)
        {
            string firstLineContent = File.ReadLines(path).First();
            return firstLineContent;
        }

        private static void rewriteFile(int[] array, string path)
        {
            string fileContent = "";
            for(int i = 0; i < array.Length - 1; i++)
            {
                fileContent = fileContent + array[i] + " ";
            }
            fileContent = fileContent + array[array.Length - 1];
            File.WriteAllText(path, fileContent);
        }

        private static int[] tryParseToIntArray(string fileContent)
        {
            string[] numbersStr = fileContent.Split(" ");
            int[] numbers = new int[numbersStr.Length];
            int i = 0;
            foreach (string number in numbersStr)
            {
                numbers[i] = Int32.Parse(number);
                i++;
            }
            return numbers;
        }

        private static void bubbleSort(int[] array)
        {
            bool isSorted = true;
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = 0; j < array.Length-1; j++)
                {
                    if(array[j] > array[j + 1])
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
            for(int i = 1; i < array.Length; i++)
            {
                int sel = array[i];
                int j = i - 1;
                while(j >= 0 && sel < array[j])
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = sel;
            }
        }

        private static void mergeSort(int[] array)
        {
            mergeSort(array, 0, array.Length-1);
        }

        private static void mergeSort(int[] array, int startIndex, int endIndex)
        {
            if(startIndex < endIndex)
            {
                int middleIndex = (startIndex + endIndex) / 2;
                mergeSort(array, startIndex, middleIndex);
                mergeSort(array, middleIndex+1, endIndex);
                merge(array, startIndex, middleIndex, endIndex);
            }
        }

        private static void merge(int[] array, int startIndex, int middleIndex, int endIndex)
        {
            int leftPointer = startIndex;
            int rigthPointer = middleIndex+1;
            int[] temporaryArray = new int[endIndex - startIndex + 1];
            int index = 0;

            while ((leftPointer <= middleIndex) && (rigthPointer <= endIndex))
            {
                if(array[leftPointer] < array[rigthPointer])
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

            for(int i = leftPointer; i <= middleIndex; i++)
            {
                temporaryArray[index] = array[i];
                index++;
            }

            for(int i = rigthPointer; i <= endIndex; i++)
            {
                temporaryArray[index] = array[i];
                index++;
            }

            for (int i = 0; i < temporaryArray.Length; i++)
            {
                array[startIndex + i] = temporaryArray[i];
            }
        }

        private static int[] generateArray(int size)
        {
            int[] array = new int[size];
            for(int i = 0; i < array.Length; i++)
            {
                int randomNumber = random.Next(int.MaxValue);
                array[i] = randomNumber;
            }
            return array;
        }

        private static void algoTest(sortArray sortingMethod)
        {
            int[] sortedArray = new int[100];
            int[] array = new int[sortedArray.Length];
            for (int i = 0; i < sortedArray.Length; i++)
            {
                sortedArray[i] = i;
                array[i] = i;
            }
            for(int i = 0; i < array.Length; i++)
            {
                int firstElem = random.Next(array.Length);
                int secondElem = random.Next(array.Length);
                int n = array[firstElem];
                array[firstElem] = array[secondElem];
                array[secondElem] = n;
            }
            sortingMethod(array);
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] != sortedArray[i])
                {
                    Console.WriteLine("Алгоритм работает некорректно");
                    return;
                }
            }
            Console.WriteLine("Алгоритм работает корректно");
        }

        private static void printArray(int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
