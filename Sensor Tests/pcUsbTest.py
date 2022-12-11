import serial
import time

device_COM = 'COM5'

device = serial.Serial(device_COM, baudrate=115200, bytesize=8, parity='N', stopbits=1, timeout=1, xonxoff=0, rtscts=0)
while True:
    response = str(device.read_all(), 'utf-8')
    if response != "":
        print(response)

    