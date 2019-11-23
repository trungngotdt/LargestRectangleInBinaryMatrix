#include <limits.h> 
#include <stdio.h> 
#include <stdlib.h> 
/*
Authors:
Trần Trung Hiếu - 51703086
Ngô Quốc Trung - 51503026
Trần Văn Tài - 51503138
*/

int findMax(int** matrix, int max, int length)
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

int main(int argc, char* argv[])
{
	const char* filename = argv[1];
	FILE* out;
	int temp, count = 0, i = 0, j = 0;
	int** array;
	FILE* in;
	in = fopen(filename, "r");
	fscanf(in, "%d", &count);
	array = (int**)malloc(count * count * sizeof(int*));
	for (i = 0;i < count;i++)
	{
		array[i] = (int*)malloc(count * sizeof(int));
	}
	int max = 0;
	int tempMax = 0;
	for (i = 0; i < count; i++)
	{
		tempMax = 0;
		for (j = 0; j < count; j++)
		{
			fscanf(in, "%d", &temp);
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
			array[i][j] = temp;
		}
	}
	fclose(in);

	max= findMax(array, max, count);
	free(array);


	

	out = fopen(argv[2], "w+");
	fprintf(out, "%d", max);
	fclose(out);
	
	return 0;
}

