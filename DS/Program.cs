using System;
using System.Threading;

namespace DS
{
    class MainClass
    {
        private static int[,] _array;
        private static int[] _array2;

        private static int _count = 0;
        private static  ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

        public static void Main(string[] args)
        {
            string line = "abbbcccddeekkkkkkhh";

            //line = TaskOne(line);
            TaskTwo();
            Console.WriteLine("\n конец");
            Console.ReadLine();
        }

        //--------------------- 1 задание ----------------------------

        public static string TaskOne(string line)
        {
            char simbol = ' ';
            int count = 1;
            string tempLine = "";
            
            for (int i = 0; i < line.Length; i++)
            {
                if (i == 0)
                {
                    tempLine += line[i];
                    simbol = line[i];
                }
                else
                {
                    if (line[i] == simbol)
                    {
                        count++;
                    }
                    else
                    {
                        if (count > 1)
                        {
                            tempLine += Convert.ToChar(count.ToString());
                        }

                        tempLine += line[i];
                        count = 1;
                        simbol = line[i];
                    }

                    if (i == line.Length - 1)
                    {
                        if (count > 1)
                        {
                            tempLine += Convert.ToChar(count.ToString());
                        }
                    }
                }
            }

            return tempLine;
        }
        
        //--------------------- 2 задание ----------------------------

        public static void TaskTwo()
        {
            int indexArray2 = 0;
            int topBorder;
            int downBorder;
            int leftBorder;
            int rightBorder;

            CreateArray();

            topBorder = 0;
            downBorder = _array.GetLength(0) - 1;
            leftBorder = 0;
            rightBorder = _array.GetLength(1) - 1;
            PrintArray(ref _array);
            _array2 = new int[_array.Length];

            while (indexArray2 != _array2.Length)
            {
                for (int i = leftBorder; i <= downBorder; i++)
                {
                    _array2[indexArray2++] = _array[i, topBorder];
                }

                leftBorder++;

                for (int i = leftBorder; i <= rightBorder; i++)
                {
                    _array2[indexArray2++] = _array[downBorder, i];
                }

                downBorder--;

                for (int i = downBorder; i >= topBorder; i--)
                {
                    _array2[indexArray2++] = _array[i, rightBorder];
                }

                rightBorder--;

                for (int i = rightBorder; i >= leftBorder; i--)
                {
                    _array2[indexArray2++] = _array[topBorder, i];
                }

                topBorder++;
            }
            Console.WriteLine();
            PrintArray(ref _array2);
        }

        private static void CreateArray()
        {
            int numb = 1;

            Console.Write("Ширина массива - ");
            int width = GetPositiveNumber();
            Console.Write("Высота массива - ");
            int height = GetPositiveNumber();

            _array = new int[height, width];

            for (int i = 0; i < _array.GetLength(0); i++)
            {
                for (int j = 0; j < _array.GetLength(1); j++)
                {
                    _array[i, j] = numb++;
                }
            }
        }

        private static void PrintArray(ref int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]}, ");
                }
                Console.WriteLine();
            }
        }

        private static void PrintArray(ref int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]}, ");
            }
        }

        private static int GetPositiveNumber()
        {
            string readName;
            bool isConversionSucceeded = true;
            int number = 0;

            while (isConversionSucceeded)
            {
                readName = Console.ReadLine();

                if (int.TryParse(readName, out number))
                {
                    if (number < 1)
                    {
                        Console.Write("Неверный ввод. Число меньше единицы. Повторите ввод - ");
                    }
                    else
                    {
                        isConversionSucceeded = false;
                    }
                }
                else
                {
                    Console.Write("Неверный ввод. Повторите ввод - ");
                }
            }

            return number;
        }

        //--------------------- 3 задание ----------------------------

        public static void TackThree()
        {
            AddCount(GetPositiveNumber());

            GetCount();

        }

        private static void AddCount(int value)
        {
            _readerWriterLock.EnterReadLock();
            _count += value;
            _readerWriterLock.ExitReadLock();
        }

        private static int GetCount()
        {
            _readerWriterLock.ExitReadLock();
            try
            {
                return _count;
            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }
        }
    }
}