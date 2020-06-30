using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.IO;
using System.Diagnostics;

namespace JeJuFarmProject
{
    delegate void ReceiveMessages();
    public partial class JeJuFarmMain : Form
    {
        Thread AcceptAndListeningThread;
        Boolean isConnected = false;
        BluetoothClient btClient;  //represent the bluetooth client connection
        BluetoothListener btListener; //represent this server bluetooth device

        JejuFarm StartJeju = new JejuFarm();
        Function Fun = new Function();
        object[,] CactusList = new string[33, 6]; //11부터, 2 품명 4 단가
        //object[,] ExcelData;
        string[] CactusName;
        int[] CactusValue;
        public int LvCount=0;
        public JeJuFarmMain()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            if (BluetoothRadio.IsSupported)
            {
                AcceptAndListeningThread = new Thread(AcceptAndListen);
                AcceptAndListeningThread.Start();
            }
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        private string FilePath = System.Environment.CurrentDirectory + @"\JejuData.ini";
        StreamReader srReceiver;
        private delegate void UpdateLogCallback(string strMessage);

        private void ReceiveMessages()
        {
            srReceiver = new StreamReader(btClient.GetStream());
            Console.WriteLine("Connected Successfully!");
            sendMessage("1053-4030\n");
            while (isConnected)
            {
                // Show the messages in the log TextBox
                string str = srReceiver.ReadLine();
                if (str == null){ // 연결이 끊기면
                    Process.Start(@"C:\Main.exe"); //Todo: 경로
                    Application.ExitThread();
                    Environment.Exit(0);
                }else { // 작업 처리
                    int tempsum = 0;
                    button6_Click(null, null);
                    int cnt = Ubound(Strings.Split(str, "!"));
                    this.Invoke(new Action(delegate ()
                    {

                        for (int i = 0; i < cnt; i++)
                        {
                            string str1 = Strings.Split(str, "!")[i]; // 1줄 값
                            Lv1.Items[i].SubItems[1].Text = Strings.Split(str1, " ")[0];
                            Lv1.Items[i].SubItems[2].Text = Strings.Split(str1, " ")[1];
                            Lv1.Items[i].SubItems[3].Text = Strings.Split(str1, " ")[2];
                            Lv1.Items[i].SubItems[4].Text = Strings.Split(str1, " ")[3];
                            tempsum += int.Parse(Lv1.Items[i].SubItems[4].Text);
                            LvCount++;

                        }
                        Lv1.Items[21].SubItems[1].Text = tempsum.ToString();
                    }));
                    button4_Click(null, null);
                }
            }
        }
        public int Ubound(String[] str)
        {
            int cnt = 0;
            try
            {
                for (int i = 0; ; i++)
                {
                    str[i].Trim();
                    cnt++;
                }
            }
            catch (Exception)
            {
                return cnt - 1;
            }
            return 0;
        }

        public void AcceptAndListen()
        {
            while (true){
                if (isConnected) {
                    try {
                        ReceiveMessages();
                    }catch (Exception e){
                        isConnected = btClient.Connected;
                        Console.WriteLine(e.Message);
                    }
                } else{
                    //TODO: if there is no connection
                    // accepting
                    try{
                        btListener = new BluetoothListener(BluetoothService.RFCommProtocol);

                        Console.WriteLine(BluetoothService.RFCommProtocol);
                        btListener.Start();
                        btClient = btListener.AcceptBluetoothClient();
                        isConnected = btClient.Connected;
                        Console.WriteLine("연결완료");
                    }catch (Exception){ }
                }
            }
        }
        //function to send message to the client
        public Boolean sendMessage(String msg)
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
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("taskkill", "/im excel.exe /f");
            StringBuilder temp = new StringBuilder(255);
            try
            {
                int ListC = 0;
                for ( ListC = 1; ; ListC++) { 
                    int ret = GetPrivateProfileString("JejuObjectData", ("OJ" + ListC).ToString(), "", temp, 255, FilePath);
                    if (ret == 0)
                    {
                        break;
                    }
                }
                ListC--;
                CactusName = new string[ListC];
                CactusValue = new int[ListC];
                for (int i = 1; ; i++)
                {
                    int ret = GetPrivateProfileString("JejuObjectData", ("OJ" + i).ToString(), "", temp, 255, FilePath);
                    if (ret == 0)
                        break;
                    else
                    {
                        CactusName[i-1] = Regex.Split(temp.ToString(), ",")[0]+"{" + i + "}";
                        CactusValue[i - 1] = Convert.ToInt32(Regex.Split(temp.ToString(), ",")[1]);
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            };
            Thread.Sleep(1000);
            StartJeju.OpenExcel(); // JejuExcel을 시작함.
            for(int i = 0; i < CactusName.Length; i++)
            {
                ObjectTxt.Items.Add(CactusName[i] + " " + CactusValue[i]);
                listBox1.Items.Add(CactusName[i] + " " + CactusValue[i]);
                listBox2.Items.Add(i+1);
            }
            for(int i=22;i<46;i++)
                listBox2.Items.Add(i);
            CactusList = StartJeju.GetExcelFrmData();

            Lv1.View = View.Details;
            Lv1.LabelEdit = false;
            Lv1.CheckBoxes = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(StartJeju.GetVisible() == true)
            {
                StartJeju.SetVisibleExcel(false);
            }
            else {
                StartJeju.SetVisibleExcel(true);
            }
        }
        public void CactusListReset()
        {
            for(int i = 11; i <= 31; i++)
            {
                for(int j = 1; j <= 6; j++)
                {
                    CactusList[i, j] = null;
                }
            }CactusList[33, 4] = "0";
        }
        public void button2_Click(object sender, EventArgs e)
        { // D33 합계
            try
            {
                if(Fun.InstrSeach(ObjectTxt.Text ,"{") != 0)
                {
                    Lv1.Items[LvCount].SubItems[1].Text = ObjectTxt.Text;
                }
                else
                {
                    Lv1.Items[LvCount].SubItems[1].Text = CactusName[Convert.ToInt32(ObjectTxt.Text)-1].ToString();
                }
                //Lv1.Items[LvCount].SubItems[1].Text = CactusName[Convert.ToInt32(ObjectTxt.Text) - 1].ToString();
                Lv1.Items[LvCount].SubItems[2].Text = NumberTxt.Text;
                Lv1.Items[LvCount].SubItems[3].Text = CactusValue[Convert.ToInt32(Regex.Split(Regex.Split(Lv1.Items[LvCount].SubItems[1].Text, "{")[1], "}")[0]) - 1].ToString();
                Lv1.Items[LvCount].SubItems[4].Text = (CactusValue[Convert.ToInt32(Regex.Split(Regex.Split(Lv1.Items[LvCount].SubItems[1].Text, "{")[1], "}")[0]) - 1] *
                Convert.ToInt32(NumberTxt.Text)).ToString();

                int Sum = 0;
                for (int i = 0; i <= LvCount; i++)
                {
                    Sum = Sum + Convert.ToInt32(Lv1.Items[i].SubItems[4].Text);
                }

                Lv1.Items[21].SubItems[1].Text = Sum.ToString();
                LvCount++;
                NumberTxt.Text = null;
                ObjectTxt.Text = null;
                ObjectTxt.Focus();
                //CactusList[11, 2] = textBox1.Text;
                //StartJeju.SetDataExcel(CactusList);
            }catch(Exception es)
            {
                Lv1.Items[LvCount].SubItems[1].Text = null;
                Lv1.Items[LvCount].SubItems[2].Text = null;
                Lv1.Items[LvCount].SubItems[2].Text = null;
                Lv1.Items[LvCount].SubItems[3].Text = null;
                Lv1.Items[LvCount].SubItems[4].Text = null;
                MessageBox.Show(es.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartJeju.EndExcel();
        }


        private void Lv1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = Lv1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void Lv1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (LvCount <= 0)
            {
                MessageBox.Show("삭제할 목록이 없습니다.");
                LvCount = 0;
            }
            else
            {

                try
                {
                    var idxColl = Lv1.SelectedIndices;
                    int idx = 0;
                    for (int i = idxColl.Count - 1; i >= 0; i--)
                        idx = idxColl[i];

                    if (idx >= LvCount)
                    {
                        MessageBox.Show("삭제하실 내용이 없습니다.");
                        return;
                    }

                    Lv1.Items[idx].SubItems[1].Text = "";
                    Lv1.Items[idx].SubItems[2].Text = "";
                    Lv1.Items[idx].SubItems[3].Text = "";

                    for (int i = idx; i <= LvCount; i++)
                    {
                        Lv1.Items[i].SubItems[1].Text = Lv1.Items[i + 1].SubItems[1].Text;
                        Lv1.Items[i].SubItems[2].Text = Lv1.Items[i + 1].SubItems[2].Text;
                        Lv1.Items[i].SubItems[3].Text = Lv1.Items[i + 1].SubItems[3].Text;
                        Lv1.Items[i].SubItems[4].Text = Lv1.Items[i + 1].SubItems[4].Text;
                    }


                    Lv1.Items[LvCount].SubItems[1].Text = "";
                    Lv1.Items[LvCount].SubItems[2].Text = "";
                    Lv1.Items[LvCount].SubItems[3].Text = "";
                    Lv1.Items[LvCount].SubItems[4].Text = "";
                    LvCount--;
                    int Sum = 0;
                    for (int i = 0; i < LvCount; i++)
                    {
                        Sum = Sum + Convert.ToInt32(Lv1.Items[i].SubItems[3].Text);
                    }

                    Lv1.Items[21].SubItems[1].Text = Sum.ToString();
                    ObjectTxt.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void button6_Click(object sender, EventArgs e)
        {
            CactusListReset();
            this.Invoke(new Action(delegate ()
            {

                for (int i = 0; i <= LvCount; i++)
                {
                    Lv1.Items[i].SubItems[1].Text = "";
                    Lv1.Items[i].SubItems[2].Text = "";
                    Lv1.Items[i].SubItems[3].Text = "";
                    Lv1.Items[i].SubItems[4].Text = "";
                }

                Lv1.Items[21].SubItems[1].Text = "0";
                LvCount = 0;
                ObjectTxt.Focus();
            }));
        }

        public void button4_Click(object sender, EventArgs e)
        {
            CactusListReset();
            int NCount = 0;
            for (int i = 0; i < LvCount; i++)
            {
                CactusList[11 + i, 1] = Strings.Split(Lv1.Items[i].SubItems[1].Text,"{")[0]; // 품명
                CactusList[11 + i, 2] = Lv1.Items[i].SubItems[2].Text; // 수량
                NCount += Convert.ToInt32(Lv1.Items[i].SubItems[2].Text);
                CactusList[11 + i, 3] = Lv1.Items[i].SubItems[3].Text; // 단가
                CactusList[11 + i, 4] = Lv1.Items[i].SubItems[4].Text; // 가격
            }
            CactusList[33, 4] = Lv1.Items[21].SubItems[1].Text;
            CactusList[9, 3] = NCount;
            CactusList[7, 3] = DateTime.Now.ToString();

            StartJeju.SetDataExcel(CactusList);
            StartJeju.SetPrintExcel();
            ObjectTxt.Focus();
        }

        private void JeJuFarmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartJeju.EndExcel();
            Application.ExitThread();
            Environment.Exit(0);
        }

        private void NumberTxt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    button2_Click(null, e);
                    break;
            }
        }

        private void NumberTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.Opacity = trackBar1.Value / 1000.0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Lv1.Items[LvCount].SubItems[1].Text = listBox1.Text;
                Lv1.Items[LvCount].SubItems[2].Text = listBox2.Text;
                Lv1.Items[LvCount].SubItems[3].Text = CactusValue[Convert.ToInt32(Regex.Split(Regex.Split(Lv1.Items[LvCount].SubItems[1].Text, "{")[1], "}")[0]) - 1].ToString();
                Lv1.Items[LvCount].SubItems[4].Text = (Convert.ToInt32(Lv1.Items[LvCount].SubItems[2].Text) * Convert.ToInt32(Lv1.Items[LvCount].SubItems[3].Text)).ToString();

                int Sum = 0;
                for (int i = 0; i <= LvCount; i++)
                {
                    Sum = Sum + Convert.ToInt32(Lv1.Items[i].SubItems[4].Text);
                }

                Lv1.Items[21].SubItems[1].Text = Sum.ToString();
                LvCount++;
                listBox1.SelectedItem = null;
                listBox2.SelectedItem = null;
                ObjectTxt.Focus();
                
            }
            catch (Exception es)
            {
                Lv1.Items[LvCount].SubItems[1].Text = null;
                Lv1.Items[LvCount].SubItems[2].Text = null;
                Lv1.Items[LvCount].SubItems[2].Text = null;
                Lv1.Items[LvCount].SubItems[3].Text = null;
                Lv1.Items[LvCount].SubItems[4].Text = null;
                MessageBox.Show(es.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "컴퓨터종료")
            {
                button8.Text = "취소";
                System.Diagnostics.Process.Start("shutdown", "/s /f /t 10");
            }
            else
            {
                button8.Text = "컴퓨터종료";
                System.Diagnostics.Process.Start("shutdown", "/a");
            }
        }
    }
}
