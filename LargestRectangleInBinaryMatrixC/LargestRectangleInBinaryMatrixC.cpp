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
	int* result;
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
	result = (int*)malloc(5 * sizeof(int));
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
			array[i][j] =i!=0&& array[i][j]!=0 ?array[i-1][j]+1:temp;
		}
	}
	fclose(in);
	return array;
}
int maina(int argc, char* argv[])
{
	
	const char* filename = "test1.txt";
#pragma warning (disable : 4996)

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
	for (i = 0; i < count; i++)
	{
		for (j = 0; j < count; j++)
		{
			fscanf(in, "%d", &temp);
			array[i][j] = i == 0 ? temp : (temp == 0 ? temp : array[i - 1][j] + 1);
		}
	}
	fclose(in);

	int r = 0; int x1 = 0; int y1 = 0; int x2 = 0; int y2 = 0; int maxSize = INT32_MIN;
	for (i = 0; i < count; i++)
	{

		int* result = _max_hist(array[i], count, r, x1, y1, x2, y2, maxSize);
		r++;
		x1 = result[0];
		y1 = result[1];
		x2 = result[2];
		y2 = result[3];
		maxSize = result[4];
	}


	FILE* out;

	out = fopen("out.txt", "w+");
	fprintf(out, "%d %d\n%d %d",x1,y1,x2,y2);
	fclose(out);
	
	



	return 0;
}

