
namespace Dieukhiendongco
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Stop = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kD = new System.Windows.Forms.TextBox();
            this.kI = new System.Windows.Forms.TextBox();
            this.kP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SetSpeed = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.TCP_receive = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(146, 127);
            this.Stop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(150, 44);
            this.Stop.TabIndex = 26;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(146, 77);
            this.Start.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(150, 44);
            this.Start.TabIndex = 25;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(302, 216);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 25);
            this.label6.TabIndex = 24;
            this.label6.Text = "current Speed (rpm)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(518, 216);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(770, 47);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 25);
            this.label5.TabIndex = 23;
            this.label5.Text = "kD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(642, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 25);
            this.label4.TabIndex = 22;
            this.label4.Text = "kI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(513, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 25);
            this.label3.TabIndex = 21;
            this.label3.Text = "kP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "Set Speed";
            // 
            // kD
            // 
            this.kD.Location = new System.Drawing.Point(776, 77);
            this.kD.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.kD.Name = "kD";
            this.kD.Size = new System.Drawing.Size(112, 31);
            this.kD.TabIndex = 19;
            this.kD.Text = "0.03";
            // 
            // kI
            // 
            this.kI.Location = new System.Drawing.Point(648, 77);
            this.kI.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.kI.Name = "kI";
            this.kI.Size = new System.Drawing.Size(112, 31);
            this.kI.TabIndex = 18;
            this.kI.Text = "0.05";
            // 
            // kP
            // 
            this.kP.Location = new System.Drawing.Point(519, 77);
            this.kP.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.kP.Name = "kP";
            this.kP.Size = new System.Drawing.Size(112, 31);
            this.kP.TabIndex = 17;
            this.kP.Text = "0.07";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(308, 127);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 44);
            this.button1.TabIndex = 16;
            this.button1.Text = "send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.send_Click);
            // 
            // SetSpeed
            // 
            this.SetSpeed.Location = new System.Drawing.Point(308, 77);
            this.SetSpeed.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SetSpeed.Name = "SetSpeed";
            this.SetSpeed.Size = new System.Drawing.Size(196, 31);
            this.SetSpeed.TabIndex = 15;
            this.SetSpeed.Text = "0";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 273);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Speed";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1188, 411);
            this.chart1.TabIndex = 27;
            this.chart1.Text = "chart1";
            // 
            // serialPort
            // 
            this.serialPort.PortName = "COM9";
            // 
            // TCP_receive
            // 
            this.TCP_receive.AutoSize = true;
            this.TCP_receive.Location = new System.Drawing.Point(894, 214);
            this.TCP_receive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TCP_receive.Name = "TCP_receive";
            this.TCP_receive.Size = new System.Drawing.Size(147, 25);
            this.TCP_receive.TabIndex = 28;
            this.TCP_receive.Text = "Data_Receive";
            this.TCP_receive.Click += new System.EventHandler(this.TCP_receive_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 703);
            this.Controls.Add(this.TCP_receive);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.kD);
            this.Controls.Add(this.kI);
            this.Controls.Add(this.kP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SetSpeed);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_onClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox kD;
        private System.Windows.Forms.TextBox kI;
        private System.Windows.Forms.TextBox kP;
        private System.Windows.Forms.TextBox SetSpeed;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.IO.Ports.SerialPort serialPort;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label TCP_receive;
        private System.Windows.Forms.Timer timer1;
    }
}

