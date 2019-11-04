#include <limits.h> 
#include <stdio.h> 
#include <stdlib.h> 

int largest(int arr[], int n)
{
	int i;
	int maxi = arr[0];
	for (i = 1; i < n; i++)
		if (arr[i] > maxi)
			maxi = arr[i];

	return maxi;
}

void setDefaultValue(int* list, int n)
{
	int i = 0;
	for (i = 0; i < n; i++)
	{
		list[i] = 0;
	}
}
int findLargestRectangle(int** array, int length)
{
	int* arr_count_row = (int*)malloc((length - 1) * sizeof(int));
	int* arr_count_col = (int*)malloc((length - 1) * sizeof(int));
	int* arr_row = (int*)malloc((length) * sizeof(int));
	int* arr_col = (int*)malloc((length) * sizeof(int));
	int* _row = (int*)malloc((length) * sizeof(int));
	int* _col = (int*)malloc((length) * sizeof(int));
	setDefaultValue(arr_count_col, length - 1);
	setDefaultValue(arr_count_row, length - 1);
	
	
	setDefaultValue(_row, length);
	setDefaultValue(_col, length);
	int count1 = 0;
	int count = 0;
	int i = 0;
	int row = 0;
	int col = 0;
	for (row = 1;row < length;row++)
	{
		count1 = 0;setDefaultValue(arr_row, length);
		
		for (col = 0; col < length; col++)
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
			printf("%d \n", arr_count_row[count]);
		}
		count++;
	}
	count = 0;

	for (col = 1;col < length;col++)
	{
		count1 = 0;setDefaultValue(arr_col, length);
		
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
	for ( i = 0; i < (length-1); i++)
	{
		printf("%d ", arr_count_col[i]);
	}
	printf("\n");
	for (i = 0; i < (length - 1); i++)
	{
		printf("%d ", arr_count_row[i]);
	}
	printf("\n");
	int dup_c;
	dup_c= 2;
	int dup_r;
	dup_r= 2;
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
	printf("%d %d", S1, S2);
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

	int largest_row;largest_row = largest(_row, length);
	int largest_col;largest_col = largest(_col, length);
	for (i = 0; i < (length ); i++)
	{
		printf("%d ", _col[i]);
	}
	for (i = 0; i < (length ); i++)
	{
		printf("%d ",_row[i]);
	}
	if ((largest_row > S1&& largest_row > S2 )|| (largest_col > S1&& largest_col > S2))
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
			array[i][j] = temp;
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
			array[i][j] = temp;
			/*i == 0 ? temp : (temp == 0 ? temp : array[i - 1][j] + 1);*/
		}
	}
	fclose(in);
	largestSize = findLargestRectangle(array, count);

	free(array);
	free(result);




	out = fopen(argv[2], "w+");
	fprintf(out, "%d", largestSize);
	fclose(out);



	return 0;
}

