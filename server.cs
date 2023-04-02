using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsApptest1
{
    
    public partial class server : Form
    {
        
        private Thread readThread;
        private Thread timelimitThread;
        private Thread ShowThread;
        private Socket socketatServer;
        private TcpListener MyListener;
        private bool ConnectOnline;
        private int P1 = 0;
        private int P3 = 4;
        private int accumulator=0;//报文段累加器
        private Stopwatch MyTimer = new Stopwatch();//确认累计计时器
        private List<MessageClass> MessageList = new List<MessageClass>();//报文段信息列表
        public server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;//去掉控件的跨线程非法访问属性
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readThread = new Thread(new ThreadStart(RunServer));
            readThread.Start();

            timelimitThread = new Thread(new ThreadStart(TimeScan));
            timelimitThread.Start();

            ShowThread = new Thread(new ThreadStart(SHOWDetail));
            ShowThread.Start();
        }
        private void RunServer()
        {
            Int32 port;
            int.TryParse(Port.Text, out port);
            MyListener = new TcpListener(IPAddress.Parse(IPAddr.Text), port);
            
            //MyListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8088);
            MyListener.Start();
            Serstatus.Text = "服务启动";
            socketatServer = MyListener.AcceptSocket();
            ConnectOnline = true;
            Serstatus.Text = "连接建立成功";

            byte[] ReceiveMessage = new byte[500];
            while (ConnectOnline)
            {
                if(socketatServer.Available>0)
                {
                    int num = socketatServer.Receive(ReceiveMessage);
                    
                    if (num > 0)
                    {
                        byte tempbyte = OutCheck(ReceiveMessage);
                        if (tempbyte != ReceiveMessage[ReceiveMessage[2] - 1])
                            MessageBox.Show("校验错误", "attention");
                        Random random = new Random();//随机数种子
                        int action = random.Next(0, 3);//随机选择本次接收的结果
                        //int action = 0;
                        byte[] tempMessage = new byte[ReceiveMessage[2]];
                        Array.Copy(ReceiveMessage, tempMessage, ReceiveMessage[2]);
                        tempMessage[0] = 32;//空格
                        tempMessage[1] = 32;
                        tempMessage[2] = 32;
                        tempMessage[ReceiveMessage[2] - 1] = 32;//校验信息位
                        if (action == 1)//丢失
                        {
                            string str = Encoding.Default.GetString(tempMessage);
                            ShowDetail.AppendText("报文：" + str + "  " + ReceiveMessage[1]+"  丢失" + "\r\n");
                            continue;
                        }
                        if (action == 2)//校验错误
                        {
                            string str = Encoding.Default.GetString(tempMessage);
                            ShowDetail.AppendText("报文：" + str + "  " + ReceiveMessage[1] + "  校验错误" + "\r\n");
                            continue;
                        }
                        if (ReceiveMessage[1] < P1 || ReceiveMessage[1] > P3)
                        {
                            continue;
                        }
                        bool IsRepeat = false;//重复报文标志
                        foreach(var item in MessageList)
                        {
                            if (item.IdentiFication == ReceiveMessage[1])
                            {
                                IsRepeat = true;
                                break;
                            }//说明这是发送端由于长时间收不到确认消息而重发的，急需确认
                        }
                        if (IsRepeat == false)
                        {
                            MessageList.Add(new MessageClass { IdentiFication = ReceiveMessage[1], IsSendReceive=false,ReceiveTime= System.DateTime.Now, message = ReceiveMessage });
                            MessageList = MessageList.OrderBy(MessageClass => MessageClass.IdentiFication).ToList();
                            accumulator++;
                            
                            string str = Encoding.Default.GetString(tempMessage);
                            ShowDetail.AppendText("收到报文：" + str + "  "+ReceiveMessage[1] + "\r\n");
                            //ShowDetail.AppendText();
                            if (accumulator != 3)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            string str = Encoding.Default.GetString(tempMessage);
                            ShowDetail.AppendText("报文：" + str + "  " + ReceiveMessage[1] + "  重复" + "\r\n");
                            continue;
                        }
                        int MaxIdentiFication = 0;
                        foreach(var item in MessageList)//查找到最大的、允许返回确认消息的报文段标识
                        {
                            MaxIdentiFication++;
                            item.IsSendReceive = true;//在之后，此报文段会被发送确认消息
                            if(MaxIdentiFication != item.IdentiFication)//发现了标识不连续
                            {
                                item.IsSendReceive = false;
                                MaxIdentiFication--;
                                break;
                            }
                        }
                        byte[] sendAck = new byte[5];
                        sendAck[0] = 1;//报文类型为确认消息
                        //sendAck[1] = 4;//测试用，ACK=4
                        sendAck[1] = IntToBitConverter(MaxIdentiFication+1)[0];//报文标识
                        int tempSize;
                        int.TryParse(SendSize.Text, out tempSize);
                        sendAck[2] = IntToBitConverter(tempSize)[0];//设置发送窗口大小
                        socketatServer.Send(sendAck);//发送确认消息
                        ShowDetail.AppendText("（累加器为3）发送ACK：" + sendAck[1] + "\r\n");
                        P1 = MaxIdentiFication + 1;
                        P3 = P1 + 4;
                        accumulator = 0;
                        MyTimer.Start();
                    }                    
                }
                Thread.Sleep(1500);
            }
        }

        private byte OutCheck(byte[] receiveMessage)
        {
            byte temp = 0;
            for (int i = 0; i < receiveMessage[2]-1; i++)
            {
                temp += receiveMessage[i];
            }
            return temp;
        }

        public byte[] IntToBitConverter(int num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            return bytes;
        }
        private void TimeScan()
        {
            while(true)
            {
                bool IsFind = false;
                foreach(var item in MessageList)
                {
                    DateTime now = System.DateTime.Now;
                    if (item.IsSendReceive==false&&((now.Minute-item.ReceiveTime.Minute>0)||(now.Second - item.ReceiveTime.Second > 9)))
                    {
                        item.ReceiveTime = now;//!!!!!!!!!!!!!!!!此处待考虑，这是为了避免短时间内重复发送
                        IsFind = true;
                        break;
                    }
                }
                if (IsFind == true)
                {
                    int MaxIdentiFication = 0;
                    foreach(var item in MessageList)//查找最大的、允许返回确认消息的报文标识
                    {
                        MaxIdentiFication++;
                        item.IsSendReceive = true;//在之后，此报文段会被发送确认消息
                        if (MaxIdentiFication != item.IdentiFication)
                        {
                            item.IsSendReceive = false;
                            MaxIdentiFication--;
                            break;
                        }
                    }
                    byte[] sendAck = new byte[5];
                    sendAck[0] = 1;//报文类型为确认消息                                
                    sendAck[1] = IntToBitConverter(MaxIdentiFication + 1)[0];
                    //sendAck[2] = 5;//设置发送窗口大小
                    int tempSize;
                    int.TryParse(SendSize.Text, out tempSize);
                    sendAck[2] = IntToBitConverter(tempSize)[0];//设置发送窗口大小
                    socketatServer.Send(sendAck);//发送确认消息
                    ShowDetail.AppendText("(超过时限)发送ACK：" + sendAck[1] + "\r\n");

                    P1 = MaxIdentiFication + 1;
                    P3 = P1 + 4;
                    accumulator = 0;
                }
                Thread.Sleep(500);
            }
            
        }
        private void SHOWDetail()
        {
            while (true)
            {
                WindLoca1.Text = "";
                for (int i = P1; i <= P3; i++)
                {
                    WindLoca1.AppendText(i + " ");
                }
                Thread.Sleep(2000);
            }

        }
        private void closeserver_Click(object sender, EventArgs e)
        {
            ConnectOnline = false;
            socketatServer.Close();
            MyListener.Stop();
            Serstatus.Text = "服务关闭";
            MessageBox.Show("服务器关闭！", "服务器");
        }
    }
    public class MessageClass
    {
        public int IdentiFication;
        public bool IsSendReceive;
        public DateTime ReceiveTime;
        public byte[] message = new byte[500];

        public MessageClass()
        {

        }
    }
}
