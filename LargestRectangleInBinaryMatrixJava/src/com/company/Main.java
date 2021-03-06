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
		int maxInRow = 0;
		int step = 1;
		int numberOfRow = 0;
		int row, column = 0;
		for (numberOfRow = length - 1; numberOfRow > 0; numberOfRow--)
		{
			step = length - numberOfRow;
			for (row = 0; row < numberOfRow; row++)
			{
				maxInRow = 0;
				for (column = 0; column < length; column++)
				{
					if (matrix[row][column] == step && matrix[row + 1][column] == step)
					{
						matrix[row][column] = step + 1;
						maxInRow += matrix[row][column];

					}
					else
					{
						if (maxInRow > max)
						{
							max = maxInRow;
						}
						matrix[row][column] = 0;
						maxInRow = 0;
					}
				}
				if (maxInRow > max)
				{
					max = maxInRow;
				}
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
