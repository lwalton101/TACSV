import tkinter as tk, serial, ctypes, matplotlib.pyplot as plt, matplotlib.image as mpimg, json, datetime
from tkinter import *
from tkinter import ttk 
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
from RepeatingTimer import RepeatingTimer

# creates connection with USB device(pico)
device_COM = 'COM3'
device = serial.Serial(device_COM, baudrate=115200, bytesize=8, parity='N', stopbits=1, timeout=1, xonxoff=0, rtscts=0)

# Allows the icon to be on the taskbar
myappid = 'T.A.C.V' # arbitrary string
ctypes.windll.shell32.SetCurrentProcessExplicitAppUserModelID(myappid)

window = tk.Tk()

graph = None
figure = None

yLimEntry = None
xLim = (0, 100)
yLim = (0, 100)

time = []
temps = []
pressures = []
temp = IntVar()
pressure = IntVar()
goblinMode = IntVar()

def getData():
    response = str(device.read_all(), 'utf-8')
    if response != "":
        responseSplit = response.split(",")
        temp = responseSplit[0]
        pressure = responseSplit[1].replace("\n","")

        temps.append(float(temp))
        pressures.append(float(pressure))
        time.append(len(time))

dataCollectionThread = RepeatingTimer(getData, 1)
dataCollectionThread.start()

def saveData():
    time = datetime.datetime.now()
    f = open(f"../Data/{time.day}-{time.month}-{time.year} {time.hour}.{time.minute}.{time.second}.json", "w")

    jsonElement = {}
    jsonElement["temps"] = temps
    jsonElement["pressures"] = pressures
    
    json.dump(jsonElement, f)



def quit_me():
    window.quit()
    window.destroy()
    dataCollectionThread.stop()
    saveData()

def refreshGraph():
    global graph
    global figure
    if graph is not None:
        plt.close(figure)
        graph.get_tk_widget().destroy()
    
    figure = plt.figure()
    ax = figure.add_subplot(111)
    
    if goblinMode.get() == 1:
        img = mpimg.imread('amin nostril.png')
        ax.imshow(img, cmap="hot")
    
    if temp.get() == 1:
        ax.plot(time, temps, "b", label="Temperature (℃)")
    if pressure.get() == 1:
        ax.plot(time, pressures, "r", label="Pressure (kPa)")

    ax.legend()

    
    graph = FigureCanvasTkAgg(figure, graphTab)
    graph.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH)
    split = yLimEntry.get().split(",")
    ax.set_ylim(float(split[0]), float(split[1]))
    ax.set_xlabel('Time')

window.geometry("1000x800")
window.wm_resizable(False, False)
window.wm_iconbitmap("amin davies 7E.ico")
window.wm_title("Team Artemis CatSat Viewer")
window.protocol("WM_DELETE_WINDOW", quit_me)

#Initalises used widgets
my_notebook= ttk.Notebook(window)
my_notebook.pack(expand=1,fill=BOTH)

homeTab= ttk.Frame(my_notebook)
my_notebook.add(homeTab, text="Home")
graphTab = ttk.Frame(my_notebook)
my_notebook.add(graphTab, text="Graphs")

Label(homeTab, text= "Home", font= ('Helvetica 20 bold')).pack()
Label(graphTab, text= "Graphs", font=('Helvetica 20 bold')).pack()

Checkbutton(graphTab, text="Temperature", variable=temp, onvalue=1, offvalue=0).pack(side=tk.BOTTOM)
Checkbutton(graphTab, text="Pressure", variable=pressure, onvalue=1, offvalue=0).pack(side=tk.BOTTOM)
Checkbutton(graphTab, text="Goblin Mode", variable=goblinMode, onvalue=1, offvalue=0).pack(side=tk.BOTTOM)

yLimEntry=Entry(graphTab, width=35)
yLimEntry.insert(0, "0,100")
yLimEntry.pack(side=tk.BOTTOM)
Button(graphTab, text="Save", command=saveData).pack(side=tk.BOTTOM)
Button(graphTab, text="Refresh", command=refreshGraph).pack(side=tk.BOTTOM)

refreshGraph()
window.mainloop()