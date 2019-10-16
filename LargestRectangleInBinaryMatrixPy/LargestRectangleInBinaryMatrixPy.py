import sys
def _max_hist(row, lengthRow,r,x1,y1 ,x2 ,y2 ,maxSize):
  heightStack=[]
  positionStack=[]
  size = 0
  tempH =-100
  tempPos = -100
  height = -100
  i = 0
  print(row)
  for i in range(0,lengthRow):
    height = row[i]
    if len( heightStack)==0:
      heightStack.append(row[i])
      positionStack.append(i)
    else:
      if height > heightStack[-1]:
        heightStack.append( row[i])
        positionStack.append( i)
      else:
        tempPos = i
        while height < heightStack[-1]:
        
          tempH = heightStack.pop()
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
  i=i+1
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

array=[]
input=open(sys.argv[1],"r")
storeInput=input.readlines()
input.close()
count=int(storeInput[0])
print(storeInput[1].split())

for i in range(1,count+1):
    strArr=[int(item) for item in storeInput[i].split()]
    #print(strArr)
    array.append(strArr)

for i in range(0,count):
    for j in range(0,count):
        if i!=0 and array[i][j]!=0:
            array[i][j]=array[i-1][j]+1
        else:
            array[i][j]=array[i][j]

r = 0
x1 = 0
y1 = 0
x2 = 0
y2 = 0
maxSize = -100

for i in range(0,count):
    result = _max_hist(array[i], count, r, x1, y1, x2, y2, maxSize)
    r=r+1
    x1 = result[0]
    y1 = result[1]
    x2 = result[2]
    y2 = result[3]
    maxSize = result[4]
output= open(sys.argv[2],"w+")

output.write(str( x1)+" "+str( y1)+" \n"+str( x2)+" "+str( y2))
output.close()
print(str( x1)+" "+str( y1)+" \n"+str( x2)+" "+str( y2))
#for i in range(0,count):
#    print(input.readline())
