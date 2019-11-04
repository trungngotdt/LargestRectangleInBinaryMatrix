# Authors:
# Trần Trung Hiếu - 51703086
# Ngô Quốc Trung - 51503026
# Trần Văn Tài - 51503138
import sys
def readFile(inFile):
    f = open(inFile, 'r')
    size = int(f.readline())
    a = [[] for _ in range(size)]

    for r in range(0, len(a)):
        line = f.readline().split(" ")
        for c in range(0, len(a)):  
            a[r].append(int(line[c]))

    return a

def WriteFile(outFile, S):
    f = open(outFile, 'w')

    f.write(str(S))
    
    f.close()

def checkRectangular(inFile, outFile):
    a = readFile(inFile)
    arr_count_row = []
    arr_count_col = []
    for _ in range(0, len(a)-1):
        arr_count_row.append(0)
        arr_count_col.append(0)
    
    #ARR_ROW DUNG DE CHUA CAC SO CAP SO 1 LIEN TIEP CUA TUNG HANG
    #ARR_COL DUNG DE CHUA CAC SO CAP SO 1 LIEN TIEP CUA TUNG COT
    arr_row = []
    arr_col = []
    for _ in range(0, len(a)):
        arr_row.append(0)
        arr_col.append(0)
    
    #ARR_LIST_ROW DUNG DE CHUA SO CAP SO 1 GIUA 2 DONG
    count = 0
    for row in range(1, len(a)):  
        count1 = 0
        for i in range(0, len(a)):
            arr_row[i] = 0
        for col in range(0, len(a)):
            if a[row][col] == 0 or a[row-1][col] == 0:
                count1 = count1 + 1
            if a[row][col] == 1 and a[row-1][col] == 1:
                arr_row[count1] = arr_row[count1] + 1
            arr_count_row[count] = max(arr_row)
            print("",arr_count_row[count])
        count = count + 1

    #ARR_LIST_COL DUNG DE CHUA SO CAP SO 1 GIUA 2 COT
    count = 0
    for col in range(1, len(a)):
        count1 = 0
        for i in range(0, len(a)):
            arr_col[i] = 0
        for row in range(0, len(a)):
            if a[row][col] == 0 or a[row][col-1] == 0:
                count1 = count1 + 1
            if a[row][col] == 1 and a[row][col-1] == 1:
                arr_col[count1] = arr_col[count1] + 1
            arr_count_col[count] = max(arr_col)
        count = count + 1
    
    print('Arr Count Row:', arr_count_row[0:])
    print('Arr Count Column:', arr_count_col[0:])
    #KIEM TRA SO LAN LAP CUA MAX TRONG 2 MANG
    dup_c = 2
    dup_r = 2
    for i in range(1, len(arr_count_row)):
        if arr_count_row[i] == max(arr_count_row) and arr_count_row[i-1] == max(arr_count_row):
            dup_r = dup_r + 1
    
    for i in range(1, len(arr_count_col)):
        if arr_count_col[i] == max(arr_count_col) and arr_count_col[i-1] == max(arr_count_col):
            dup_c = dup_c + 1
    
    S1 = max(arr_count_row) * dup_r
    S2 = max(arr_count_col) * dup_c

    print(S1, S2)

    #TRUONG HOP HCN CO CHIEU DAI HOAC CHIEU RONG BANG 1
    row_ = []
    col_ = []
    for _ in range(0, len(a)):
        row_.append(0)
        col_.append(0)
        
    for row in range(0, len(a)):
        for col in range(0, len(a)):
            if a[row][col] == 1:
                row_[row] = row_[row] + 1
    for col in range(0, len(a)):
        for row in range(0, len(a)):
            if a[row][col] == 1:
                col_[col] = col_[col] + 1
    
    print(row_[0:])
    print(col_[0:])
    
    if max(row_) > S1 and max(row_) > S2 or max(col_) > S1 and max(col_) > S2:
        if max(row_) >= max(col_):
            WriteFile(outFile, max(row_))
        else:
            WriteFile(outFile, max(col_))
    else:
        if S1 >= S2:
            WriteFile(outFile, S1)
        else:
            WriteFile(outFile, S2)
        
    

checkRectangular(sys.argv[1], sys.argv[2])
