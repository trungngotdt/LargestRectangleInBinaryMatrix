
def _max_hist(row, lengthRow,r,x1,y1 ,x2 ,y2 ,maxSize):

	heightStack =[]
	positionStack =[]
	size = 0
	tempH =-100
	tempPos = -100
	height = -100
	i = 0
    for x in range(0,lengthRow-1):
        height = row[i]
		if len( heightStack)==0:
		
			heightStack.append( row[i])
			positionStack.append( i)		
		else:
		
			if height > heightStack[-1]:
			
				push(heightStack, row[i])
				push(positionStack, i)
			
			else:
			
				tempPos = i
				while height < heightStack[-1]:
				
					tempH = pop(heightStack)
					tempPos = positionStack.pop()
					size = tempH * (i - tempPos)
					if maxSize < size:
					
						maxSize = size
						y2 = i - 1
						x2 = r
						x1 = r - tempH + 1
						y1 = tempPos
					
					if len( heightStack)==0:
					
						break
				heightStack.append( row[i])
				positionStack.append(tempPos)
    while len( heightStack)!=0:
	
		tempH = heightStack.pop()
		tempPos = positionStack.pop()
		size = tempH * (i - tempPos)
		if maxSize < size:
		
			maxSize = size
			y2 = i - 1
			x2 = r
			x1 = r - tempH + 1
			y1 = tempPos
		
	
	result=[0]*5
	result[0] = x1
	result[1] = y1
	result[2] = x2
	result[3] = y2
	result[4] = maxSize
	return result

A[4][4] =[[0, 1, 1, 0],[1, 2, 2, 1],[2, 3, 3, 2],[3, 4, 0, 0]]
B[5][5] =[[1,0,0, 1, 1],[0,0, 1, 2, 2],[1, 1, 2, 0,0],[0,2, 3, 1,1],[0,3, 4, 2,2]]
r = 0
x1 = 0
y1 = 0
x2 = 0
y2 = 0
maxSize = -100
for i in range(0,4-1):
    result = _max_hist(A[i], 4, r, x1, y1, x2, y2, maxSize)
	r=r+1
	x1 = result[0]
	y1 = result[1]
	x2 = result[2]
	y2 = result[3]
	maxSize = result[4]
	

print( x1, y1, x2, y2,maxSize)



r = 0
x1 = 0
y1 = 0
x2 = 0
y2 = 0
maxSize = -100
for i in range(0,5-1):
	result = _max_hist(B[i], 5, r, x1, y1, x2, y2, maxSize)
	r=r+1
	x1 = result[0]
	y1 = result[1]
	x2 = result[2]
	y2 = result[3]
	maxSize = result[4]
	

print( x1, y1, x2, y2,maxSize)

