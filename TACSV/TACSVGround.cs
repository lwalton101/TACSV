using System;
using System.Diagnostics;
using System.IO.Ports;

namespace TACSV;

public class TACSVGround
{
    private SerialPort _port;
    public event EventHandler<string> OnMessageRecieved;
    private string lineBeingRead = "";
    
    public string ComPort
    {
        get => _port.PortName;
        set => _port.PortName = value;
    }

    public int BaudRate
    {
        get => _port.BaudRate;
        set => _port.BaudRate = value;
    }
    
    public int ReadTimeout
    {
        get => _port.ReadTimeout;
        set => _port.ReadTimeout = value;
    }

    public TACSVGround()
    {
        _port = new SerialPort();
        _port.DtrEnable = true;
        _port.DataReceived += OnDataRecieved;
    }

    private void OnDataRecieved(object sender, SerialDataReceivedEventArgs e)
    {
        lineBeingRead += _port.ReadExisting();
        if (lineBeingRead.EndsWith("\n"))
        {
            lineBeingRead = lineBeingRead.Trim();
            Trace.WriteLine($"We got a line ender: {lineBeingRead}");
            OnMessageRecieved?.Invoke(this, lineBeingRead);
            lineBeingRead = "";
        }
    }

    public void Connect()
    {
        _port.Open();
    }

}