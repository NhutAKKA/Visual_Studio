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
        bool isBusy = false;
        bool isMotorStop = true; 
       
        string data = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           


            timer1.Enabled = true;


            serialPort.DtrEnable = true;
            ////khi arduino giao tiep qua USB, thi DTR phai duoc bat thi ham datareceived moi duoc goi.

            serialPort.Open();

            if (serialPort.IsOpen)
            {
                Console.WriteLine("Port is opened");
            }
            serialPort.WriteLine("vs_stop");
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();

            serialPort.DataReceived += SerialPort_DataReceived;
            //binding event

            iteration = 300;
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

                using(var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    
                    while((data = reader.ReadLine()) != null)
                    {
                        switch (data)
                        {
                            case "Stop":
                                if (isMotorStop)
                                {
                                    break;
                                }

                                serialPort.WriteLine("vs_stop");
                                isMotorStop = true;

                                break;
                            case "Start":
                                serialPort.WriteLine("vs_start");
                                isMotorStop = false;

                                break;
                            default:

                                Invoke(new Action(() =>
                                {
                                    SetSpeed.Text = data.Replace("motor_power", "");
                                }));
                                data = "vs_set_speed" + data.Replace("motor_power", "");
                                Console.WriteLine(data);
                                serialPort.WriteLine(data);
                                break;
                        }
                    }

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
            //send set_speed to Arduino


            serialPort.WriteLine("vs_kp" + kP.Text);//send kP to Arduino   
            serialPort.WriteLine("vs_ki" + kI.Text);//send kI to Arduino
            serialPort.WriteLine("vs_kd" + kD.Text);//send kD to Arduino
        }


 

        private void Form1_onClosing(object sender, FormClosingEventArgs e)
        {
            serialPort.WriteLine("vs_stop");
            Console.WriteLine("CLosing serial port");
            serialPort.Close();
           

        }


    }
}
