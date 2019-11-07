import sys
def findMax(matrix, max, length):
    maxLocal=0
    step = 1
    i=0
    j = 0
    while i < length:
        maxLocal = 0
        if i == (length - step):
            step=step+1
            i = 0
        if length == step:
            break
        j=0
        while j<length:            
            if matrix[i][j] == step and matrix[i + 1][j] == step:
                matrix[i][j] = step + 1
                maxLocal =maxLocal+ matrix[i][j]
            else :
                if maxLocal > max:
                    max = maxLocal
                matrix[i][j] = 0
                maxLocal = 0
            j=j+1
        if maxLocal > max:
            max = maxLocal
        i=i+1
       
    
    return max

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
