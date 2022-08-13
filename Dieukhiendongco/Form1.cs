using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Dieukhiendongco
{
    public partial class Form1 : Form
    {
        int iteration;
        string speed;
       
        String data = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();


            timer1.Enabled = true;


            //serialPort.DtrEnable = true;
            ////khi arduino giao tiep qua USB, thi DTR phai duoc bat thi ham datareceived moi duoc goi.

            //serialPort.Open();

            //if (serialPort.IsOpen)
            //{
            //    Console.WriteLine("Port is opened");
            //}

            //serialPort.DataReceived += SerialPort_DataReceived;
            ////binding event

            //iteration = 300;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Int32 port = 8081;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            Console.WriteLine("init TCP");

            Byte[] bytes = new Byte[256];

            while (true)
            {
                Console.WriteLine("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                var stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    //Console.WriteLine("Received: {0}", data);

                }

            }
        }

        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string dataFromArduino = serialPort.ReadLine();

            if (dataFromArduino.Length < 5 || dataFromArduino.Substring(0, 5) != "speed")
            {
                return;
            }

            speed = dataFromArduino.Substring(5, dataFromArduino.Length - 6);

            Invoke(new Action(() =>
            {
                label1.Text = dataFromArduino;

                this.chart1.Series["Speed"].Points.AddXY(iteration, Convert.ToDouble(speed));
                iteration++;
                this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = iteration - 300;
            }));


        }

        private void send_Click(object sender, EventArgs e)
        {
            serialPort.WriteLine("vs_set_speed" + SetSpeed.Text);//send set_speed to Arduino


            serialPort.WriteLine("vs_kp" + kP.Text);//send kP to Arduino   
            serialPort.WriteLine("vs_ki" + kI.Text);//send kI to Arduino
            serialPort.WriteLine("vs_kd" + kD.Text);//send kD to Arduino
        }


        private void Start_Click(object sender, EventArgs e)
        {
            serialPort.WriteLine("vs_start");
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            serialPort.WriteLine("vs_stop");
        }



        private void Form1_onClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("CLosing serial port");
            serialPort.Close();
           

        }

        private void TCP_receive_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //data
            if (data == null || data.Length == 0) 
            {
                return;
            }


            Console.WriteLine(data);
        }
    }
}
