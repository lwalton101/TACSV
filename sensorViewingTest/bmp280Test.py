import board, busio, adafruit_bmp280, time, usb_cdc

if usb_cdc.data is None:
    print("Need to enable USB CDC serial data in boot.py!")
    while True:
        pass
usbl = usb_cdc.data

def send_data(data):
    data = bytes(data+"\n", 'utf-8')
    usbl.write(data)

usb_cdc.data.timeout = 5

i2c = busio.I2C(scl = board.GP15, sda = board.GP14)
bmp280_sensor = adafruit_bmp280.Adafruit_BMP280_I2C(i2c, address=0x76)

def readTemperature():
    return bmp280_sensor.temperature

def readPressure():
    return bmp280_sensor.pressure

while True:
    print("Temp: " + str(readTemperature()))
    print("Pressure: " + str(readPressure()) + "kpa")
    send_data(str(readTemperature()) + "," + str(readPressure() / 10))
    time.sleep(1)
    

        
