using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    class Program
    {
        /// <summary>
        ///         y
        ///     +--------->
        ///  x  |
        ///     |
        ///     |
        ///    \|/
        ///     v
        /// index 0:x1
        /// index 1:y1
        /// index 2:x2
        /// index 3:y2
        /// index 4:max size
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static int[] _max_hist(int[] row, int r = 0, int x1 = 0, int y1 = 0, int x2 = 0, int y2 = 0, int maxSize = int.MinValue)
        {
            var heightStack = new Stack<int>();
            var positionStack = new Stack<int>();
            var length = row.Length;
            int size = 0;
            var tempH = int.MinValue;
            var tempPos = int.MinValue;
            var height = int.MinValue;
            int i = 0;
            for (i = 0; i < length; i++)
            {
                height = row[i];
                if (heightStack.Count == 0)
                {
                    heightStack.Push(row[i]);
                    positionStack.Push(i);
                }
                else
                {
                    if (height > heightStack.Peek())
                    {
                        heightStack.Push(row[i]);
                        positionStack.Push(i);
                    }
                    else
                    {
                        tempPos = i;
                        while (height < heightStack.Peek())
                        {
                            tempH = heightStack.Pop();
                            tempPos = positionStack.Pop();
                            size = tempH * (i - tempPos);
                            if (maxSize < size)
                            {
                                maxSize = size;
                                y2 = i - 1;
                                x2 = r;
                                x1 = r - tempH + 1;
                                y1 = tempPos;
                            }
                            if (heightStack.Count == 0)
                            {
                                break;
                            }
                        }

                        heightStack.Push(row[i]);
                        positionStack.Push(tempPos);
                    }
                }
            }
            while (heightStack.Count != 0)
            {
                tempH = heightStack.Pop();
                tempPos = positionStack.Pop();
                size = tempH * (i - tempPos);
                if (maxSize < size)
                {
                    maxSize = size;
                    y2 = i - 1;
                    x2 = r;
                    x1 = r - tempH + 1;
                    y1 = tempPos;
                }
            }

            return new int[] { x1, y1, x2, y2, maxSize };
        }
        public static void Main(string[] args)
        {
            int[][] A = new int[][]
            {
                new int[] {0, 1, 1, 0},
                new int[] {1, 2, 2, 1},
                new int[] {2, 3, 3, 2},
                new int[] {3, 4, 0, 0}
            };//1, 0, 2, 3
            int[][] B = new int[][]
            {
                new int[] {1,0,0, 1, 1},
                new int[] {0,0, 1, 2, 2},
                new int[] {1, 1, 2, 0,0},
                new int[] {0,2, 3, 1,1},
                new int[] {0,3, 4, 2,2}
            };//3, 1, 4, 4
            int r = 0; int x1 = 0; int y1 = 0; int x2 = 0; int y2 = 0; int maxSize = int.MinValue;
            foreach (var item in A)
            {
                var result = _max_hist(item, r, x1, y1, x2, y2, maxSize);
                r++;
                x1 = result[0];
                y1 = result[1];
                x2 = result[2];
                y2 = result[3];
                maxSize = result[4];
            }
            Console.WriteLine($"{x1},{y1},{ x2}, {y2},{maxSize}");
            r = 0; x1 = 0; y1 = 0; x2 = 0; y2 = 0; maxSize = int.MinValue;
            foreach (var item in B)
            {
                var result = _max_hist(item, r, x1, y1, x2, y2, maxSize);
                r++;
                x1 = result[0];
                y1 = result[1];
                x2 = result[2];
                y2 = result[3];
                maxSize = result[4];
            }
            Console.WriteLine($"{x1},{y1},{ x2}, {y2},{maxSize}");
            Console.WriteLine("Hello World!");
        }
    }
}
