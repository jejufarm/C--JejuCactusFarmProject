using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BluetoothCore
{
    public class Bluetooth
    {
        private delegate void UpdateLogCallback(string strMessage);
        private BluetoothClient btClient;  //represent the bluetooth client connection
        private BluetoothListener btListener; //represent this server bluetooth device
        private Thread AcceptAndListeningThread;
        private StreamReader srReceiver;
        private bool isConnected = false;

        public Bluetooth()
        {
            if (BluetoothRadio.IsSupported)
            {
                AcceptAndListeningThread = new Thread(AcceptAndListen);
                AcceptAndListeningThread.Start();
            }
        }

        public void AcceptAndListen()
        {
            while (true)
            {
                if (isConnected)
                {
                    try
                    {
                        ReceiveMessages();
                    }
                    catch (Exception e)
                    {
                        isConnected = btClient.Connected;
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    //TODO: if there is no connection
                    // accepting
                    try
                    {
                        btListener = new BluetoothListener(BluetoothService.RFCommProtocol); 

                        Console.WriteLine(BluetoothService.RFCommProtocol);
                        btListener.Start();
                        btClient = btListener.AcceptBluetoothClient();
                        isConnected = btClient.Connected;
                        Console.WriteLine("연결완료");
                    }
                    catch (Exception) { }
                }
            }
        }
        private void ReceiveMessages()
        {
            srReceiver = new StreamReader(btClient.GetStream());

            sendMessage("1053-4030\n");
            while (isConnected)
            {
                // Show the messages in the log TextBox
                string str = srReceiver.ReadLine();
                if (str == null)
                { // 연결이 끊기면
                    Environment.Exit(0);
                }
                else
                { // 작업 처리
                    
                }
            }
        }
        public bool sendMessage(String msg)
        {
            try
            {
                if (!msg.Equals(""))
                {
                    UTF8Encoding encoder = new UTF8Encoding();
                    NetworkStream stream = btClient.GetStream();
                    stream.Write(encoder.GetBytes(msg + "\n"), 0, encoder.GetBytes(msg).Length);
                    stream.Flush();

                }
            }
            catch (Exception)
            {
                try
                {
                    isConnected = btClient.Connected;
                    btClient.GetStream().Close();
                    btClient.Dispose();
                    btListener.Server.Dispose();
                    btListener.Stop();
                }
                catch (Exception)
                {
                }

                return false;
            }

            return true;
        }
    }
}
