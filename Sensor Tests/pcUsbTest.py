import serial, time, json

device_COM = 'COM3'
tempList = []
pressureList = []
dataFile = open("data.json", "w")
jsonElement = {}

device = serial.Serial(device_COM, baudrate=115200, bytesize=8, parity='N', stopbits=1, timeout=1, xonxoff=0, rtscts=0)
for x in range(0,50):
    response = str(device.read_all(), 'utf-8')
    if response != "":
        responseSplit = response.split(",")
        temp = responseSplit[0]
        pressure = responseSplit[1].replace("\n","")

        tempList.append(float(temp))
        pressureList.append(float(pressure))
    time.sleep(1)
jsonElement["temps"] = tempList
jsonElement["pressure"] = pressureList
json.dump(jsonElement, dataFile)


    