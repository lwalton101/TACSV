using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TACSV
{
    class PiPicoManager
    {
        private static SerialPort _port;

        public static void ConnectToPort(string portName)
        {
            if (!portName.StartsWith("COM"))
            {
                MessageBox.Show($"Cannot connect to port {portName} that doesn't begin with COM");
            }

            _port = new SerialPort();
            _port.PortName = portName;
            _port.BaudRate = 9600;
        }
    }
}
