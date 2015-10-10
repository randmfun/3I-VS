using System;
using System.IO.Ports;
using log4net;

namespace SCD_UVSS.SystemInput.COM
{
    public class ComPortProvider: IComPortProvider
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ComPortProvider));

        public SerialPort SerialPort { get; set; }

        public bool StopComConnection;

        public ComPortProvider(SerialPort serialPort)
        {
            this.SerialPort = serialPort;

            //8bits, no parity, no hand shake, 1 stop bit, 115kbps
            this.SerialPort.BaudRate = 9600;
            this.SerialPort.Parity = Parity.None;
            this.SerialPort.StopBits = StopBits.One;
            this.SerialPort.DataBits = 8;
            this.SerialPort.Handshake = Handshake.None;
            
            // Set the read/write timeouts
            this.SerialPort.ReadTimeout = 500;
            this.SerialPort.WriteTimeout = 500;
        }

        public string Read()
        {
            try
            {
                this.OpenPort();
                var readContent = this.SerialPort.ReadLine();
                return readContent;
            }
            catch (TimeoutException exception)
            {
                Logger.Error("Read throw time out exception!", exception);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Error("Read throws general exception!", exception);
                throw;
            }
        }

        public void Write(string message)
        {
            try
            {
                this.OpenPort();
                this.SerialPort.WriteLine(message);

            }
            catch (TimeoutException exception)
            {
                Logger.Error("Write throw time out exception!", exception);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Error("Write throws general exception!",exception);
                throw;
            }
        }

        private void OpenPort()
        {
            try
            {
                if(!this.SerialPort.IsOpen)
                    this.SerialPort.Open();
            }
            catch (Exception exception)
            {
                Logger.Error("Open Com Port failed!", exception);
                throw;
            }
        }

        public void ClosePort()
        {
            try
            {
                if (this.SerialPort.IsOpen)
                    this.SerialPort.Close();
            }
            catch (Exception exception)
            {
                Logger.Error("Close Com Port failed!", exception);
                throw;
            }
        }
    }
}
