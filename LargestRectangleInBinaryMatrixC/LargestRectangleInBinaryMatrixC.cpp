// LargestRectangleInBinaryMatrixC.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <limits.h> 
#include <stdio.h> 
#include <stdlib.h> 

// A structure to represent a stack 
struct Stack {
	int top;
	unsigned capacity;
	int* array;
};

// function to create a stack of given capacity. It initializes size of 
// stack as 0 
struct Stack* createStack(unsigned capacity)
{
	struct Stack* stack = (struct Stack*)malloc(sizeof(struct Stack));
	stack->capacity = capacity;
	stack->top = -1;
	stack->array = (int*)malloc(stack->capacity * sizeof(int));
	return stack;
}

// Stack is full when top is equal to the last index 
int isFull(struct Stack* stack)
{
	return stack->top == stack->capacity - 1;
}

// Stack is empty when top is equal to -1 
int isEmpty(struct Stack* stack)
{
	return stack->top == -1;
}

// Function to add an item to stack.  It increases top by 1 
void push(struct Stack* stack, int item)
{
	if (isFull(stack))
		return;
	stack->array[++stack->top] = item;
}

// Function to remove an item from stack.  It decreases top by 1 
int pop(struct Stack* stack)
{
	if (isEmpty(stack))
		return INT_MIN;
	return stack->array[stack->top--];
}

// Function to return the top from stack without removing it 
int peek(struct Stack* stack)
{
	if (isEmpty(stack))
		return INT_MIN;
	return stack->array[stack->top];
}

bool canPeek(struct Stack* stack)
{
	if (isEmpty(stack))
	{
		return false;
	}
	return true;
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
int* _max_hist(int* row, int lengthRow, int r = 0, int x1 = 0, int y1 = 0, int x2 = 0, int y2 = 0, int maxSize = INT32_MIN)
{
	struct Stack* heightStack = createStack(lengthRow);
	struct Stack* positionStack = createStack(lengthRow);
	int size = 0;
	int tempH = INT32_MIN;
	int tempPos = INT32_MIN;
	int height = INT32_MIN;
	int i = 0;
	for (i = 0; i < lengthRow; i++)
	{
		height = row[i];
		if (isEmpty(heightStack))
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
					if (isEmpty(heightStack))
					{
						break;
					}
				}

				push(heightStack, row[i]);
				push(positionStack, tempPos);
			}
		}
	}
	while (!isEmpty(heightStack))
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
	int result[5];
	result[0] = x1;
	result[1] = y1;
	result[2] = x2;
	result[3] = y2;
	result[4] = maxSize;
	return result;
}

int** ReadFileAndTranfer(const char* filename)
{

#pragma warning (disable : 4996)

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
			array[i][j] =i==0? temp:(temp==0?temp:array[i-1][j]+1);
		}
	}
	fclose(in);
	return array;
}
int main()
{
	int** AA = ReadFileAndTranfer("test1.txt");
	
	/*
	int r = 3, c = 4, i, j, count;

	int** arr = (int**)malloc(r * sizeof(int*));
	for (i = 0; i < r; i++)
		arr[i] = (int*)malloc(c * sizeof(int));

	// Note that arr[i][j] is same as *(*(arr+i)+j) 
	count = 0;
	for (i = 0; i < r; i++)
		for (j = 0; j < c; j++)
			arr[i][j] = ++count;
	*/
	
	int A[4][4] =
	{
		{0, 1, 1, 0},
		{1, 2, 2, 1},
		{2, 3, 3, 2},
		{3, 4, 0, 0}
	};
	int B[5][5] = 
	{
		{1,0,0, 1, 1},
		{0,0, 1, 2, 2},
		{1, 1, 2, 0,0},
		{0,2, 3, 1,1},
		{0,3, 4, 2,2}
	};
	int r = 0; int x1 = 0; int y1 = 0; int x2 = 0; int y2 = 0; int maxSize = INT32_MIN;

	for (int i = 0; i < 4; i++)
	{
		int* result = _max_hist(A[i], 4, r, x1, y1, x2, y2, maxSize);
		r++;
		x1 = result[0];
		y1 = result[1];
		x2 = result[2];
		y2 = result[3];
		maxSize = result[4];
	}

	printf("A result : \nx1 : %d y1 : %d x2 : %d y2 : %d max size : %d\n", x1, y1, x2, y2,maxSize);



	 r = 0; x1 = 0; y1 = 0; x2 = 0; y2 = 0; maxSize = INT32_MIN;

	for (int i = 0; i < 5; i++)
	{
		int* result = _max_hist(B[i], 5, r, x1, y1, x2, y2, maxSize);
		r++;
		x1 = result[0];
		y1 = result[1];
		x2 = result[2];
		y2 = result[3];
		maxSize = result[4];
	}

	printf("B result : \nx1 : %d y1 : %d x2 : %d y2 : %d max size : %d ", x1, y1, x2, y2,maxSize);

	return 0;
}

