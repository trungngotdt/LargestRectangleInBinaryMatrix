//package com.company;

//Authors:
//Tran Trung Hieu - 51703086
//Ngo Quoc Trung - 51503026
//Tran Van Tai - 51503138

import java.io.*;
import java.util.*;

public class Main {
    static int findMax(int[][] matrix, int max, int length)
    {
        int maxLocal = 0;
        int step = 1;
        int i, j = 0;
        for (i = 0; i < length; i++)
        {
            maxLocal = 0;
            if (i == (length - step))
            {
                step++;
                i = 0;
            }
            if (length == step)
            {
                break;
            }
            for (j = 0; j < length; j++)
            {
                if (matrix[i][j] == step && matrix[i + 1][j] == step)
                {
                    matrix[i][j] = step + 1;
                    maxLocal += matrix[i][j];
                }
                else
                {
                    if (maxLocal > max)
                    {
                        max = maxLocal;
                    }
                    matrix[i][j] = 0;
                    maxLocal = 0;
                }
            }
            if (maxLocal > max)
            {
                max = maxLocal;
            }
        }
        return max;
    }

    static ArrayList ReadFile(String path) throws IOException {
        File file = new File(path);
        int max=0;
        int tempMax=0;
        BufferedReader br = new BufferedReader(new FileReader(file));
        int count = Integer.parseInt(br.readLine());
        String st;
        String[] arraySt;
        int[][] result = new int[count][count];
        int length = 0;
        for (int i = 0; i < count; i++) {
            arraySt = br.readLine().split(" ");
            length = arraySt.length;
            tempMax=0;
            for (int j = 0; j < length; j++) {
                int temp = Integer.parseInt(arraySt[j]);
                if (temp==1)
                {
                    tempMax = tempMax + 1;
                }
                else
                {
                    tempMax = 0;
                }
                if (tempMax>max)
                {
                    max = tempMax;
                }
                result[i][j] =temp;
            }
        }
        br.close();
        ArrayList list=new ArrayList();
        list.add(result);
        list.add(max);
        return list;
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
            ArrayList list = ReadFile(args[0]);
            int[][] matrix=(int[][])list.get(0);
            int maxSize=(int)list.get(1);
            maxSize= findMax(matrix,maxSize,matrix[0].length);
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
