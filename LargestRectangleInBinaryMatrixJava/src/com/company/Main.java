//package com.company;

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
        File file = new File(path);

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
                result[i][j] = i != 0 && temp != 0 ? result[i - 1][j] + 1 : temp;
            }
        }
        br.close();

        return result;
    }

    static void WriteFile(String path, String content) {
        try (
                FileWriter writer = new FileWriter(path);
                BufferedWriter bw = new BufferedWriter(writer)) {

            bw.write(content);
            bw.close();
        } catch (IOException e) {
            System.err.format("IOException: %s%n", e);
        }
    }

    public static void main(String[] args) {

        try {
            int[][] matrix = ReadFile(args[0]);
            int r = 0;
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int maxSize = Integer.MIN_VALUE;

            for (int[] item : matrix) {
                int[] maxRectangle = _max_hist(item, r, x1, y1, x2, y2, maxSize);
                r++;
                x1 = maxRectangle[0];
                y1 = maxRectangle[1];
                x2 = maxRectangle[2];
                y2 = maxRectangle[3];
                maxSize = maxRectangle[4];
            }
            StringBuilder builder = new StringBuilder();
            builder.append(maxSize);
            String content = builder.toString();
            WriteFile(args[1], content);

        } catch (IOException ex) {
            ex.printStackTrace();
        }


        //System.out.println("a");
    }
}
