# Authors:
# Trần Trung Hiếu - 51703086
# Ngô Quốc Trung - 51503026
# Trần Văn Tài - 51503138

import sys
def findMax(matrix, maxFinal, length):
    maxLocal = 0
    step = 1
    numberOfRow = 0
    row=0
    column = 0
    for numberOfRow in range(length-1,0,-1):
       step = length - numberOfRow
       for row in range(0,numberOfRow):
           maxLocal = 0
           for column in range(0,length):
                if matrix[row][column] == step  and  matrix[row + 1][column] == step:
                   matrix[row][column] = step + 1
                   maxLocal += matrix[row][column]
                else:                
                   maxFinal= max(maxFinal, maxLocal)                    
                   matrix[row][column] = 0
                   maxLocal = 0
           maxFinal= max(maxFinal, maxLocal)
    return maxFinal

array=[]
input=open(sys.argv[1],"r")
storeInput=input.readlines()
input.close()
count=int(storeInput[0])

for i in range(1,count+1):
    strArr=[int(item) for item in storeInput[i].split()]
    #print(strArr)
    array.append(strArr)

max=0

for i in range(0,count):
    tempMax = 0
    for j in range(0,count):
        if array[i][j]==1:
            tempMax = tempMax + 1
        else:
            tempMax = 0
        if tempMax>max:
            max = tempMax

max=findMax(array,max,count)
output= open(sys.argv[2],"w+")

output.write(str( max))
output.close()
#for i in range(0,count):
#    print(input.readline())
