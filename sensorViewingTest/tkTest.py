import tkinter as tk
from tkinter import *
from tkinter import ttk
import matplotlib.pyplot as plt
import matplotlib.image as mpimg
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import json
import ctypes


myappid = 'mycompany.myproduct.subproduct.version' # arbitrary string
ctypes.windll.shell32.SetCurrentProcessExplicitAppUserModelID(myappid)

window = tk.Tk()
graph = None
figure = None
yLimEntry = None
xLim = (0, 100)
yLim = (0, 100)
dataFile = json.load(open("Data.json", "r"))
temps = dataFile["temps"]
pressures = dataFile["pressure"]
temp = IntVar()
pressure = IntVar()
goblinMode = IntVar()
time = []
for x in range(len(temps)):
    time.append(1 * x)

def quit_me():
    window.quit()
    window.destroy()

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

    
    graph = FigureCanvasTkAgg(figure, tab2)
    graph.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH)
    split = yLimEntry.get().split(",")
    ax.set_ylim(float(split[0]), float(split[1]))
    ax.set_xlabel('Time')


window.geometry("1000x800")
window.wm_resizable(False, False)
window.wm_iconbitmap("amin davies 7E.ico")
window.wm_title("Team Artemis CatSat Viewer")
window.protocol("WM_DELETE_WINDOW", quit_me)

my_notebook= ttk.Notebook(window)
my_notebook.pack(expand=1,fill=BOTH)

tab1= ttk.Frame(my_notebook)
my_notebook.add(tab1, text= "Home")
tab2= ttk.Frame(my_notebook)
my_notebook.add(tab2, text= "Graphs")
#Create a Label in Tabs
Label(tab1, text= "Tab 1", font= ('Helvetica 20 bold')).pack()
Label(tab2, text= "Tab 2", font=('Helvetica 20 bold')).pack()

Checkbutton(tab2, text="Temperature", variable=temp, onvalue=1, offvalue=0).pack(side=tk.BOTTOM)
Checkbutton(tab2, text="Pressure", variable=pressure, onvalue=1, offvalue=0).pack(side=tk.BOTTOM)
Checkbutton(tab2, text="Goblin Mode", variable=goblinMode, onvalue=1, offvalue=0).pack(side=tk.BOTTOM)

yLimEntry=Entry(tab2, width=35)
yLimEntry.insert(0, "0,100")
yLimEntry.pack(side=tk.BOTTOM)
Button(tab2, text="Refresh", command=refreshGraph).pack(side=tk.BOTTOM)

refreshGraph()



window.mainloop()