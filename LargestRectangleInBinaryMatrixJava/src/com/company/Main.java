package com.company;

import java.io.*;
import java.util.*;

public class Main {
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
    public static int[] _max_hist(int[] row, int r, int x1, int y1, int x2, int y2, int maxSize) {
        Stack<Integer> heightStack = new Stack<Integer>();
        Stack<Integer> positionStack = new Stack<Integer>();
        int length = row.length;
        int size = 0;
        int tempH = Integer.MIN_VALUE;
        int tempPos = Integer.MIN_VALUE;
        int height = Integer.MIN_VALUE;
        int i = 0;
        for (i = 0; i < length; i++) {
            height = row[i];
            if (heightStack.isEmpty()) {
                heightStack.push(row[i]);
                positionStack.push(i);
            } else {
                if (height > heightStack.peek()) {
                    heightStack.push(row[i]);
                    positionStack.push(i);
                } else {
                    tempPos = i;
                    while (height < heightStack.peek()) {
                        tempH = heightStack.pop();
                        tempPos = positionStack.pop();
                        size = tempH * (i - tempPos);
                        if (maxSize < size) {
                            maxSize = size;
                            y2 = i - 1;
                            x2 = r;
                            x1 = r - tempH + 1;
                            y1 = tempPos;
                        }
                        if (heightStack.isEmpty()) {
                            break;
                        }
                    }

                    heightStack.push(row[i]);
                    positionStack.push(tempPos);
                }
            }
        }
        while (!heightStack.isEmpty()) {
            tempH = heightStack.pop();
            tempPos = positionStack.pop();
            size = tempH * (i - tempPos);
            if (maxSize < size) {
                maxSize = size;
                y2 = i - 1;
                x2 = r;
                x1 = r - tempH + 1;
                y1 = tempPos;
            }
        }

        return new int[]{x1, y1, x2, y2, maxSize};
    }

    static int[][] ReadFile(String path) throws IOException {
        File file = new File("C:\\Users\\Admin\\Documents\\Git\\LargestRectangleInBinaryMatrix\\LargestRectangleInBinaryMatrixC\\test1.txt");

        BufferedReader br = new BufferedReader(new FileReader(file));
        int count = Integer.parseInt(br.readLine());
        String st;
        String[] arraySt;
        int[][] result = new int[count][count];
        int length = 0;
        for (int i = 0; i < count; i++) {
            arraySt = br.readLine().split(" ");
            length = arraySt.length;
            for (int j = 0; j < length; j++) {
                int temp = Integer.parseInt(arraySt[j]);
                result[i][j] = i == 0 ? temp : (temp == 0 ? temp : result[i - 1][j] + 1);
            }
        }
        return result;
    }

    static void WriteFile(String path, String content) {
        try (
                FileWriter writer = new FileWriter(path);
                BufferedWriter bw = new BufferedWriter(writer)) {

            bw.write(content);

        } catch (IOException e) {
            System.err.format("IOException: %s%n", e);
        }
    }

    public static void main(String[] args) throws IOException {
        int[][] resulta = ReadFile("");

        int[][] A = new int[][]
                {
                        new int[]{0, 1, 1, 0},
                        new int[]{1, 2, 2, 1},
                        new int[]{2, 3, 3, 2},
                        new int[]{3, 4, 0, 0}
                };//1, 0, 2, 3
        int[][] B = new int[][]
                {
                        new int[]{1, 0, 0, 1, 1},
                        new int[]{0, 0, 1, 2, 2},
                        new int[]{1, 1, 2, 0, 0},
                        new int[]{0, 2, 3, 1, 1},
                        new int[]{0, 3, 4, 2, 2}
                };//3, 1, 4, 4
        int r = 0;
        int x1 = 0;
        int y1 = 0;
        int x2 = 0;
        int y2 = 0;
        int maxSize = Integer.MIN_VALUE;

        for (int[] item : resulta) {
            int[] result = _max_hist(item, r, x1, y1, x2, y2, maxSize);
            r++;
            x1 = result[0];
            y1 = result[1];
            x2 = result[2];
            y2 = result[3];
            maxSize = result[4];
        }
        StringBuilder builder = new StringBuilder();
        builder.append(x1);
        builder.append(' ');
        builder.append(y1);
        builder.append("\n");
        builder.append(x2);
        builder.append(' ');
        builder.append(y2);
        String sss = builder.toString();
        WriteFile("tes.txt",sss);
        System.out.println("a");
    }
}