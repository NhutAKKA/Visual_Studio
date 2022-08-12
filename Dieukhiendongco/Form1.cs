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
        string cmd_read;
        string speed;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 8081;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.


                        // Send back a response.

                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();



            serialPort.DtrEnable = true;
            //khi arduino giao tiep qua USB, thi DTR phai duoc bat thi ham datareceived moi duoc goi.

            serialPort.Open();

            if (serialPort.IsOpen)
            {
                Console.WriteLine("Port is opened");
            }

            serialPort.DataReceived += SerialPort_DataReceived;
            //binding event

            iteration = 300;
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
    }
}
