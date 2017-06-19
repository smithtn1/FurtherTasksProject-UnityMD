#.txt file to .xls file (formatting may need some adjustment once experimental data is analyzed)
#Taylor Smith
#June 12 2017

import xlwt

data = []
accData = []
values = []
inc = 0

filename = raw_input("What is the name of the text file: ")
file = raw_input("What would you like the Excel file to be called: ")

f = open(str(filename), "r")


preData = f.readlines()

for x in preData:
    data = x.split(";")
    
for x in data:
    accData.append(x.split(","))

    
for x in range(len(accData)):
    
    values.append(accData[x][1:])
    

wb = xlwt.Workbook()
ws = wb.add_sheet('A test sheet')

ws.write(0, 0, "Subject ID")
ws.write(0, 1, "Age")
ws.write(0, 2, "Gender")
ws.write(0, 3, "Condition")
ws.write(0, 4, "Data")

ws.write(1, 0, (values[0][0]))
ws.write(1, 1, (values[1][0]))
ws.write(1, 2, (values[2][0]))
ws.write(1, 3, (values[3][0]))

for x in range(len(values[4])):
    ws.write(1+inc, 4, str(values[4][x]))
    inc += 1

wb.save(file + ".xls")