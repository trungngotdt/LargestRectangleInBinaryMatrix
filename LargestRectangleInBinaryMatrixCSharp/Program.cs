using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LargestRectangleInBinaryMatrixCSharp
{
    class Program
    {

        static int largest(int[] arr, int n)
        {
            int i;
            int maxi = arr[0];
            for (i = 1; i < n; i++)
                if (arr[i] > maxi)
                    maxi = arr[i];

            return maxi;
        }

        static void setDefaultValue(int[] list, int n)
        {
            int i = 0;
            for (i = 0; i < n; i++)
            {
                list[i] = 0;
            }
        }

        static int findLargestRectangle(int[][] array, int length)
        {
            int[] arr_count_row = new int[(length - 1)];// (int*)malloc((length - 1) * sizeof(int));
            int[] arr_count_col = new int[length - 1]; ;//(int*)malloc((length - 1) * sizeof(int));
            int[] arr_row = new int[length];//(int*)malloc((length) * sizeof(int));
            int[] arr_col = new int[length];//(int*)malloc((length) * sizeof(int));
            int[] _row = new int[length]; //(int*)malloc((length) * sizeof(int));
            int[] _col = new int[length];// (int*)malloc((length) * sizeof(int));
            setDefaultValue(arr_count_col, length - 1);
            setDefaultValue(arr_count_row, length - 1);


            setDefaultValue(_row, length);
            setDefaultValue(_col, length);
            int count1 = 0;
            int count = 0;
            int i = 0;
            int row = 0;
            int col = 0;
            int leftRow = 0;
            int rightRow = 0;
            int tempMaxRow = 0;
            for (row = 1; row < length; row++)
            {
                count1 = 0; setDefaultValue(arr_row, length);

                for (col = 0; col < length; col++)
                {
                    if (array[row][col] == 0 || array[row - 1][col] == 0)
                    {
                        count1++;
                    }
                    if (array[row][col] == 1 && array[row - 1][col] == 1)
                    {
                        arr_row[count1] = arr_row[count1] + 1;
                        if (arr_row[count1]>tempMaxRow)
                        {
                            tempMaxRow = arr_row[count1];
                            if (count1!=0)
                            {
                                leftRow = count1+arr_row[count1-1];
                            }
                        }
                    }
                    arr_count_row[count] = largest(arr_row, length);
                }
                count++;
            }
            count = 0;

            for (col = 1; col < length; col++)
            {
                count1 = 0; setDefaultValue(arr_col, length);

                for (row = 0; row < length; row++)
                {
                    if (array[row][col] == 0 || array[row][col - 1] == 0)
                    {
                        count1++;
                    }
                    if (array[row][col] == 1 && array[row][col - 1] == 1)
                    {
                        arr_col[count1] = arr_col[count1] + 1;
                    }
                    arr_count_col[count] = largest(arr_col, length);
                }
                count++;
            }
            int dup_c;
            dup_c = 2;
            int dup_r;
            dup_r = 2;
            for (i = 1; i < (length - 1); i++)
            {
                if ((arr_count_row[i] == largest(arr_count_row, length - 1)) && (arr_count_row[i - 1] == largest(arr_count_row, length - 1)))
                {
                    dup_r = dup_r + 1;
                }
            }
            for (i = 1; i < (length - 1); i++)
            {
                if ((arr_count_col[i] == largest(arr_count_col, length - 1)) && (arr_count_col[i - 1] == largest(arr_count_col, length - 1)))
                {
                    dup_c = dup_c + 1;
                }
            }
            int S1;
            S1 = largest(arr_count_row, length - 1) * dup_r;
            int S2;
            S2 = largest(arr_count_col, length - 1) * dup_c;
            for (row = 0; row < length; row++)
            {
                for (col = 0; col < length; col++)
                {
                    if (array[row][col] == 1)
                    {
                        _row[row] = _row[row] + 1;
                    }
                }
            }
            for (col = 0; col < length; col++)
            {
                for (row = 0; row < length; row++)
                {
                    if (array[row][col] == 1)
                    {
                        _col[col] = _col[col] + 1;
                    }
                }
            }

            int largest_row; largest_row = largest(_row, length);
            int largest_col; largest_col = largest(_col, length);
            if ((largest_row > S1 && largest_row > S2) || (largest_col > S1 && largest_col > S2))
            {
                if (largest_row > largest_col)
                {
                    return largest_row;
                }
                else
                {
                    return largest_col;
                }
            }
            else
            {
                if (S1 > S2)
                {
                    return S1;
                }
                else
                {
                    return S2;
                }
            }


        }

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
        private static string CreateRowTableString(string[] para)
        {
            StringBuilder builder = new StringBuilder();
            int length = para.Count();
            for (int i = 0; i < length; i++)
            {
                builder.Append("|");
                var countChar = para[i].Count();
                int padLeft = (12 - countChar) / 2;
                int padRight = ((12 - countChar) % 2) == 0 ? padLeft : padLeft + 1;
                builder.Append(para[i].PadLeft(padLeft).PadRight(padRight));
            }
            builder.Append("|");

            return builder.ToString();
        }
        public static void Main(string[] args)
        {
            var barTop = CreateRowTableString(new string[] { "File", "Case", "Result", "Details" });
            var cc = "echo \"|\"";
            int[][] C = new int[][]
            {
                new int[] {0, 1, 1, 0},
                new int[] {1, 1, 1, 1},
                new int[] {1, 1, 1, 1},
                new int[] {1, 1, 0, 0}
            };
            int[][] D = new int[][]
            {
                new int[] {0, 0, 0, 0,0,0},
                new int[] {1, 1, 0, 1,1,1},
                new int[] {1, 1, 0, 1,1,1},
                new int[] {0, 0, 0, 0,0,0},
                new int[] {0, 0, 0, 1,1,0},
                new int[] {0, 0, 0, 1,1,0}
            };
            var CC = findLargestRectangle(D, D.Length);
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
