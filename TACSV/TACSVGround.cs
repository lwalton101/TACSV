using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows;

namespace TACSV;

public class TACSVGround
{
    private SerialPort _port;
    public bool IsOpen => _port.IsOpen;
    public event EventHandler<string> OnMessageRecieved;
    public event EventHandler OnConnectionOpen;
    public event EventHandler OnConnectionClosed;

    private string _lineBeingRead = "";
    private bool _lastConnectionStatus;

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
		_port.ErrorReceived += (sender, e) => Trace.WriteLine(e.ToString());

        var connectionCheckingThread = new Thread(CheckForLostConnection);
        connectionCheckingThread.IsBackground = false;
        connectionCheckingThread.Start();
    }

    private void CheckForLostConnection()
    {
        while (true)
        {
            if (_lastConnectionStatus && !_port.IsOpen)
            {
                TACSVConsole.Log("Connection Lost");
                Application.Current.Dispatcher.Invoke(() => OnConnectionClosed?.Invoke(this, EventArgs.Empty));
            }
            _lastConnectionStatus = _port.IsOpen;
            //THIS IS A WIERD RACE CONDITION
            //DO NOT REMOVE THIS LINE, PORT CLOSING IS NOT DETECTED OTHERWISE
            Thread.Sleep(100);
		}
    }

    private void OnDataRecieved(object sender, SerialDataReceivedEventArgs e)
    {
        _lineBeingRead += _port.ReadExisting();
        if (_lineBeingRead.EndsWith("\n"))
        {
            _lineBeingRead = _lineBeingRead.Trim();
            TACSVConsole.Log($"Message Recieved: {_lineBeingRead}");
            OnMessageRecieved?.Invoke(this, _lineBeingRead);
            _lineBeingRead = "";
        }
    }

    public void Connect()
    {
        _port.Open();
        TACSVConsole.Log($"Connected on {ComPort}");
        OnConnectionOpen?.Invoke(this, EventArgs.Empty);
    }
}