#include <limits.h> 
#include <stdio.h> 
#include <stdlib.h> 

int largest(int arr[], int n)
{
	int i;
	int largest = arr[0];
	for (i = 1; i < n; i++)
		if (arr[i] > largest)
			largest = arr[i];

	return largest;
}

int ff(int** array, int length)
{
	int* arr_count_row = (int*)malloc((length - 1) * sizeof(int));
	int* arr_count_col = (int*)malloc((length - 1) * sizeof(int));
	int* arr_row = (int*)malloc((length) * sizeof(int));
	int* arr_col = (int*)malloc((length) * sizeof(int));
	int* _row = (int*)malloc((length) * sizeof(int));
	int* _col = (int*)malloc((length) * sizeof(int));
	int count = 0;
	int i = 0;
	for (int row = 1;row < length;row++)
	{
		int count1 = 0;
		for (i = 0; i < length; i++)
		{
			arr_row[i] = 0;
			for (int col = 0; col < length; col++)
			{
				if (array[row][col] == 0 || array[row - 1][col] == 0)
				{
					count1++;
				}
				if (array[row][col] == 1 && array[row - 1][col] == 1)
				{
					arr_row[count1] = arr_row[count1] + 1;
				}
				arr_count_row[count] = largest(arr_row, length);
			}
		}
		count++;
	}
	count = 0;

	for (int col = 1;col < length;col++)
	{
		int count1 = 0;
		for (i = 0; i < length; i++)
		{
			arr_col[i] = 0;
			for (int row = 0; col < length; col++)
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
		}
		count++;
	}
	int dup_c = 2;
	int dup_r = 2;
	for (i = 0; i < (length - 1); i++)
	{
		if (arr_count_row[i] == largest(arr_count_row, length - 1) && arr_count_row[i - 1] == largest(arr_count_row, length - 1))
		{
			dup_r = dup_r + 1;
		}
	}
	for (i = 0; i < (length - 1); i++)
	{
		if (arr_count_col[i] == largest(arr_count_col, length - 1) && arr_count_col[i - 1] == largest(arr_count_col,length-1))
		{
			dup_c = dup_c + 1;
		}
	}
	int S1 = largest(arr_count_row, length - 1) * dup_r;
	int S2 = largest(arr_count_col, length - 1) * dup_c;
	for (int row = 0; row < length; row++)
	{
		for (int col = 0; col < length; col++)
		{
			if (array[row][col]==1)
			{
				_row[row] = _row[row] + 1;
			}
		}
	}
	for (int col = 0; col < length; col++)
	{
		for (int row = 0; row < length; row++)
		{
			if (array[row][col] == 1)
			{
				_col[col] = _col[col] + 1;
			}
		}
	}

	if (largest(_row, length) > S1&& largest(_row, length) > S2 || largest(_col, length) > S1&& largest(_col, length) > S2)
	{
		return  max(largest(_row, length), largest(_col, length));
	}	
	else
	{
		return max(S1, S2);
	}
		

}

int** ReadFileAndTranfer(const char* filename)
{


	int c, temp, count = 0, i = 0, j = 0;
	int** array;
	FILE* in;
	in = fopen(filename, "r");
	fscanf(in, "%d", &count);
	array = (int**)malloc(count * count * sizeof(int*));
	for (i = 0;i < count;i++)
	{
		array[i] = (int*)malloc(count * sizeof(int));
	}
	i = 0;
	for (i = 0; i < count; i++)
	{
		for (j = 0; j < count; j++)
		{
			fscanf(in, "%d", &temp);
			array[i][j] = i != 0 && temp != 0 ? array[i - 1][j] + 1 : temp;
		}
	}
	fclose(in);
	return array;
}
int main(int argc, char* argv[])
{
	int* result;
	const char* filename = argv[1];
	FILE* out;
	int temp, count = 0, i = 0, j = 0;
	int** array;
	FILE* in;
	int r = 0, x1 = 0, y1 = 0, x2 = 0, y2 = 0, largestSize = -100;
	in = fopen(filename, "r");
	fscanf(in, "%d", &count);
	array = (int**)malloc(count * count * sizeof(int*));
	for (i = 0;i < count;i++)
	{
		array[i] = (int*)malloc(count * sizeof(int));
	}
	for (i = 0; i < count; i++)
	{
		for (j = 0; j < count; j++)
		{
			fscanf(in, "%d", &temp);
			array[i][j] = i == 0 ? temp : (temp == 0 ? temp : array[i - 1][j] + 1);
		}
	}
	fclose(in);


	for (i = 0; i < count; i++)
	{

		result = _largest_hist(array[i], count, r, x1, y1, x2, y2, largestSize);
		r++;
		x1 = result[0];
		y1 = result[1];
		x2 = result[2];
		y2 = result[3];
		largestSize = result[4];
	}
	free(array);
	free(result);




	out = fopen(argv[2], "w+");
	fprintf(out, "%d %d\n%d %d", x1, y1, x2, y2);
	fclose(out);

	return 0;
}

