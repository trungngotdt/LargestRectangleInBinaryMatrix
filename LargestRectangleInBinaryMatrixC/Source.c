#include <limits.h> 
#include <stdio.h> 
#include <stdlib.h> 

struct Stack {
	int top;
	unsigned capacity;
	int* array;
};

struct Stack* createStack(unsigned capacity)
{
	struct Stack* stack = (struct Stack*)malloc(sizeof(struct Stack));
	stack->capacity = capacity;
	stack->top = -1;
	stack->array = (int*)malloc(stack->capacity * sizeof(int));
	return stack;
}

int isFull(struct Stack* stack)
{
	return stack->top == stack->capacity - 1;
}
 
int isEmpty(struct Stack* stack)
{
	if (stack->top==-1)
	{
		return 1;
	}
	return 0;
}

void push(struct Stack* stack, int item)
{
	if (isFull(stack))
		return;
	stack->array[++stack->top] = item;
}

int pop(struct Stack* stack)
{
	if (isEmpty(stack))
		return INT_MIN;
	return stack->array[stack->top--];
}

int peek(struct Stack* stack)
{
	if (isEmpty(stack)==1)
		return INT_MIN;
	return stack->array[stack->top];
}

int canPeek(struct Stack* stack)
{
	if (isEmpty(stack))
	{
		return 0;
	}
	return 1;
}

int* _max_hist(int* row, int lengthRow, int r, int x1 , int y1, int x2 , int y2 , int maxSize)
{
	int* result;
	struct Stack* heightStack = createStack(lengthRow);
	struct Stack* positionStack = createStack(lengthRow);
	int size = 0;
	int tempH = -100;
	int tempPos = -100;
	int height = -100;
	int i = 0;
	for (i = 0; i < lengthRow; i++)
	{
		height = row[i];
		if (isEmpty(heightStack)==1)
		{
			push(heightStack, row[i]);
			push(positionStack, i);
		}
		else
		{
			if (height > peek(heightStack))
			{
				push(heightStack, row[i]);
				push(positionStack, i);
			}
			else
			{
				tempPos = i;
				while (height < peek(heightStack))
				{
					tempH = pop(heightStack);
					tempPos = pop(positionStack);
					size = tempH * (i - tempPos);
					if (maxSize < size)
					{
						maxSize = size;
						y2 = i - 1;
						x2 = r;
						x1 = r - tempH + 1;
						y1 = tempPos;
					}
					if (isEmpty(heightStack)==1)
					{
						break;
					}
				}

				push(heightStack, row[i]);
				push(positionStack, tempPos);
			}
		}
	}
	while (isEmpty(heightStack)==0)
	{
		tempH = pop(heightStack);
		tempPos = pop(positionStack);
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
	free(heightStack->array);
	free(positionStack->array);
	result= (int*)malloc(5 * sizeof(int));
	result[0] = x1;
	result[1] = y1;
	result[2] = x2;
	result[3] = y2;
	result[4] = maxSize;
	return result;
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
			array[i][j] = i != 0 && array[i][j] != 0 ? array[i - 1][j] + 1 : temp;
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
	int r = 0, x1 = 0, y1 = 0, x2 = 0, y2 = 0, maxSize = -100;
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

		result = _max_hist(array[i], count, r, x1, y1, x2, y2, maxSize);
		r++;
		x1 = result[0];
		y1 = result[1];
		x2 = result[2];
		y2 = result[3];
		maxSize = result[4];
	}
	free(array);
	free(result);


	

	out = fopen(argv[1], "w+");
	fprintf(out, "%d %d\n%d %d", x1, y1, x2, y2);
	fclose(out);
	
	return 0;
}

