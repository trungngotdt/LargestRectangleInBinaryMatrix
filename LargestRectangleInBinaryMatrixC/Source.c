#include <limits.h> 
#include <stdio.h> 
#include <stdlib.h> 


int findMax(int** matrix, int max, int length)
{
	int maxLocal;
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

