using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Microsoft.VisualBasic;
using ProgramCore.ObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BluetoothCore
{
    public delegate void PrintMessage(List<BasketListForm> list);
    public delegate void SendMessage(string msg);
    public class Bluetooth
    {
        public SendMessage send;
        public PrintMessage print;

        private BluetoothClient btClient;  //represent the bluetooth client connection
        private BluetoothListener btListener; //represent this server bluetooth device
        private Thread AcceptAndListeningThread;
        private StreamReader srReceiver;

        public Bluetooth()
        {
            if (BluetoothRadio.IsSupported)
            {
                AcceptAndListeningThread = new Thread(AcceptAndListen);
                AcceptAndListeningThread.IsBackground = true;
                AcceptAndListeningThread.Start();
            }
        }

        public void AcceptAndListen()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("연결 대기중");
                    btListener = new BluetoothListener(BluetoothService.RFCommProtocol);
                    btListener.Start();
                    btClient = btListener.AcceptBluetoothClient();
                    Console.WriteLine("연결 완료");
                    send("bluetooth_connect");
                    while (btClient.Connected)
                    {
                        Console.WriteLine("수신 대기중");
                        ReceiveMessages();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("AcceptAndListen() : " + ex.Message);
            }
        }
        private void ReceiveMessages()
        {
            try
            {
                sendMessage("1053-4030\n");
                srReceiver = new StreamReader(btClient.GetStream());
                while (btClient.Connected)
                {
                    // Show the messages in the log TextBox
                    string str = srReceiver.ReadLine();
                    if (str == null)
                    { // 연결이 끊기면
                        Console.WriteLine("연결이 끊김");
                        btClient.GetStream().Close();
                        btClient.Dispose();
                        btListener.Server.Dispose();
                        btListener.Stop();
                        AcceptAndListeningThread.Interrupt();
                        send("bluetooth_disconnect");
                        return;
                    }
                    else
                    {
                        if(str == "")
                        {
                            //Console.WriteLine("Ping Test");
                        }
                        else
                        {
                            try
                            {
                                List<BasketListForm> temp = new List<BasketListForm>();
                                int idx = 1;
                                foreach (var item in Strings.Split(str, "\\"))
                                {
                                    if (item == "")
                                        continue;
                                    string title = Strings.Split(item, " ")[0];
                                    int count = Convert.ToInt32(Strings.Split(item, " ")[1]);
                                    int price = Convert.ToInt32(Strings.Split(item, " ")[2]);
                                    temp.Add(new BasketListForm()
                                    {
                                        Index = idx++,
                                        Title = title,
                                        Count = count,
                                        Price = price,
                                        Total = (count * price)

                                    });
                                }
                                print(temp);

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("ReceiveMessages-Print : " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReceiveMessages() : " + ex.Message);
                return;
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
            catch (Exception ex)
            {
                send("bluetooth_disconnect");
                Console.WriteLine("sendMessage()" + ex.Message);
                try
                {
                    btClient.GetStream().Close();
                    btClient.Dispose();
                    btListener.Server.Dispose();
                    btListener.Stop();
                }
                catch (Exception) { }
                return false;
            }

            return true;
        }
    }
}
