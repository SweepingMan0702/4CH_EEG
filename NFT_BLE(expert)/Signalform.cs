using LiveCharts;
using LiveCharts.Wpf;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using System.Net;


//charts 
using System.Windows.Forms.DataVisualization.Charting;

//FFT
using MathNet.Numerics.IntegralTransforms;

//Complex number
using System.Numerics;
using System.Timers;

using System.IO;
using System.Management;

namespace NFT_BLE_expert_
{
    public partial class Signalform : MaterialForm
    {
        //每一封包152 每一封包18點 2 channel 每一點數 4 bytes
        //DataConvert_152  Voltage_18

        public User_form Mainform;

        /* user information */
        string User_comport;
        Boolean comport_connection;


        /* COMPORT */
        // Boolean connection = false;
        private SerialPort SerialPort1;
        int datastop = 0;
        byte[] datestart = new byte[10];
        byte[] stop = new byte[4];
        string[] Portscom;

        /* TIMER */
        private System.Windows.Forms.Timer timerRealTimeData;

        /*status*/
        private bool drawNFTstatus = false;
        private bool drawstatus = false;
        private bool getstatus = false;
        private bool round_status = false;
        private bool filestatus = false;
        private bool Openfilestatus = false;
        private bool resultstatus = false;



        /* Parameter */
        float NFT_pattern_energy;
        float score;
        private static int Packagesize = 232;//每一封包大小
        private static int Channel = 4;//幾個channel
        private static int Datapoint = 13;//每一封包點數
        private static int Point_size = 4;//每一點數
        private static int Startbytessize = 20;//檔頭大小
        private static int Endbytessize = 4;//檔尾大小
        private static int samplerate = 250;//每秒點數
        byte[] ReadBuffer = new byte[Packagesize];
        byte[] BufferMiss = new byte[Packagesize];
        byte[] BufferMSB = new byte[Packagesize];
        byte[] BufferLSB = new byte[Packagesize];
        string[] readBufferdatatString = new string[Packagesize * 2];




        /* Signal */
        private static int Newsignalsize = 130;//每一次新加入計算點數(用每一封包的點數的倍數)
        private static int signalfiltersize = Newsignalsize * 4;//每一次濾波計算點數(用每一封包的點數的倍數)
        private static int moveignalsize = signalfiltersize - Newsignalsize;
        float[] Temp_CH1 = new float[Newsignalsize];
        float[] Draw_CH1 = new float[Newsignalsize];
        float[] Temp_CH2 = new float[Newsignalsize];
        float[] Draw_CH2 = new float[Newsignalsize];
        float[] Temp_CH3 = new float[Newsignalsize];
        float[] Draw_CH3 = new float[Newsignalsize];
        float[] Temp_CH4 = new float[Newsignalsize];
        float[] Draw_CH4 = new float[Newsignalsize];
        int count = 0;//計算是否已達到要計算的點數
        int Datapoint_count = 0;//目前這一回合總點數
        int NFTpoint_count = 0;//目前這一回合總點數
        private float[] Data_CH1 = new float[signalfiltersize];//要進行濾波的資料
        private float[] Data_CH2 = new float[signalfiltersize];
        private float[] Data_CH3 = new float[signalfiltersize];//要進行濾波的資料
        private float[] Data_CH4 = new float[signalfiltersize];
        private int pointIndex = 0;
        int order1 = 15;
        int order2 = 125;
        //int order1 = 250;
        //int order2 = 15;


        //環境雜訊判斷
        float[] Enviorment_cal_ch1 = new float[250];
        float[] Enviorment_cal_ch2 = new float[250];
        double[] Enviorment_FFT_CH1 = new double[125];
        double[] Enviorment_FFT_CH2 = new double[125];
        float[] EnviormentBuffer_ch1 = new float[500];
        float[] EnviormentBuffer_ch2 = new float[500];

        //FFT
        float[] NFTBuffer_ch1 = new float[(int)(Newsignalsize * 3)];
        float[] NFTBuffer_ch2 = new float[(int)(Newsignalsize * 3)];


        float[] NFTcal_ch1 = new float[250];
        float[] NFTcal_ch2 = new float[250];



        double[] FFT_CH1 = new double[125];
        double[] FFT_CH2 = new double[125];

        //能量大小
        float pattern_energy_CH1;
        float pattern_energy_CH2;

        //原始資料存檔
        StreamWriter Rawdatafile;
        string strRawdatafile;

        //分數資料存檔
        //StreamWriter Scoredatafile;
        //string strScorefile;


        //環境雜訊大小
        float pattern_environment_CH1;
        float pattern_environment_CH2;
        float environment_temp2;
        float environment_temp1;
        int environment = 1; //環境電壓 1:60 2:50


        //NFT result
        int nftpoint = 0;
        float NFT_Pattern_energy_sum = 0;

        //  眨眼移動
        Boolean eyeblinking = true; //有眨眼為FALSE

        //畫圖
        List<LiveCharts.WinForms.CartesianChart> FFT_chart_list = new List<LiveCharts.WinForms.CartesianChart>();
        private static int Drawsignalsize = 130;//每一次新畫的點數(用每一封包的點數的倍數)
        Chart chart1 = new RealtimeChart().GetChart;
        Chart chart2 = new RealtimeChart().GetChart;
        Chart chart3 = new RealtimeChart().GetChart;
        Chart chart4 = new RealtimeChart().GetChart;//建立物件 (加this.Controls.Add(chart1);)
        float timeshow = 2.5f;//圖表上顯示資料的長度

        //Thread
        //thread
        ThreadStart record_T;
        Thread record_Thread;

        /* Class */
        private Filter filter;

        feedback feedback_form;

        int system_time = 0; // 目前訓練秒數
        int train_time = 360; // 訓練時間
        int blink_times = 0; // 眨眼次數
        int error_times = 0; // 訊號異常次數
        float total_score = 0; // 總分

        public Signalform()
        {
            InitializeComponent();
            //美化介面
            MaterialSkin.MaterialSkinManager manager = MaterialSkin.MaterialSkinManager.Instance;
            manager.AddFormToManage(this);
            manager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Blue800, MaterialSkin.Primary.Blue900, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.LightBlue400, MaterialSkin.TextShade.WHITE);

            //原始訊號繪圖部分
            panel2.Controls.Add(chart1);
            signal_max.Text = "100";
            signal_min.Text = "-100";
            panel_CH2.Controls.Add(chart2);
            signal2_max.Text = "100";
            signal2_min.Text = "-100";
            panel_CH3.Controls.Add(chart3);
            signal3_max.Text = "100";
            signal3_min.Text = "-100";
            panel_CH4.Controls.Add(chart4);
            signal4_max.Text = "100";
            signal4_min.Text = "-100";


            frequency_chart.Series.Clear();

            frequency_chart.Series.Add(new LineSeries
            {
                Title = "Ch1",
                Values = new ChartValues<double> { }
            });

            frequency_chart.Series.Add(new LineSeries
            {
                Title = "Ch2",
                Values = new ChartValues<double> { }
            });

            frequency_chart.AxisX = new LiveCharts.Wpf.AxesCollection()
            {
                new LiveCharts.Wpf.Axis()
                {
                    Separator = new LiveCharts.Wpf.Separator()
                    {
                        Step = 1.0
                    }
                }
            };

            /*
            // 宣告myIntLists 為List

            frequency_chart.Series.Add(new LineSeries
            {
                Values = new ChartValues<double> { },
                PointGeometry = null
            });
            //各頻段繪圖
            //FFT_chart_list.Add(frequency_chart);

            LineSeries series = new LineSeries
            {
                Values = new ChartValues<float> { 0 },
                StrokeThickness = 2,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 152, 0)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 4
            };
            LineSeries series1 = new LineSeries
            {
                Values = new ChartValues<float> { 0 },
                StrokeThickness = 2,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(80, 140, 240)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 4
            };
            FFT_chart_list[0].Series.Add(series);
            FFT_chart_list[0].Series.Add(series1);
            FFT_chart_list[0].AxisX.Add(new LiveCharts.Wpf.Axis
            {
                MinValue = 2,
                MaxValue = 26,
            });
            FFT_chart_list[0].AxisY.Add(new LiveCharts.Wpf.Axis
            {
                MinValue = 0,
                MaxValue = 0.00001,
            });*/


            filter = new Filter(signalfiltersize, Newsignalsize, order1, order2);


            // initialize feedback form 
            feedback_form = new feedback();
            feedback_form.Show();
        }


        /* user information */
        /*     public string Username
             {
                 set
                 {
                     User_name = value;
                     label_username.Text = "User Name : :" + User_name;
                 }
             }
          */
        public string Usercom
        {
            set
            {
                do
                {
                    User_comport = value;
                } while (User_comport == null);

            }

        }


        public Boolean Connection
        {
            set
            {
                //Connection = value;
                comport_connection = value;
                /* comport connect */
                if (value == true)
                {
                    timerRealTimeData.Enabled = true;
                    label_status.Text = "COM Status : 連線成功";
                    do
                    {
                        SerialPort1 = CreateComport(SerialPort1, User_comport);
                    } while (User_comport == null);
                    OpenComport(SerialPort1);
                }
                if (value == false)
                {
                    timerRealTimeData.Enabled = false;
                    label_status.Text = "COM Status : 中斷連線";
                    if (SerialPort1 != null)
                    {
                        CloseComport(SerialPort1);
                        SerialPort1.Dispose();
                    }

                }
            }
        }

        public Boolean Status
        {
            set
            {
                round_status = value;
                /* comport connect */
                if (value == true)
                {
                    do
                    {
                        SerialPort1.DiscardInBuffer();
                        if (datastop == 1)
                        {
                            stop[0] = 0x55;
                            stop[1] = 0x02;
                            stop[2] = 0x00;
                            stop[3] = 0xAA;
                            SerialPort1.Write(stop, 0, 4);
                            datastop = 0;
                            Thread.Sleep(1000); //Delay 1秒
                        }
                        datastop = 1;
                    } while (SerialPort1.BytesToRead != 0);
                    do
                    {
                        datestart[0] = 0x55;
                        datestart[1] = 0x03;
                        datestart[2] = 0x06;
                        datestart[3] = (byte)(DateTime.Now.Year - 2000);
                        datestart[4] = (byte)(DateTime.Now.Month);
                        datestart[5] = (byte)(DateTime.Now.Day);
                        datestart[6] = (byte)(DateTime.Now.Hour);
                        datestart[7] = (byte)(DateTime.Now.Minute);
                        datestart[8] = (byte)(DateTime.Now.Second);
                        datestart[9] = 0xAA;
                        if (datastop == 1)
                        {
                            datastop = 0;
                        }
                        SerialPort1.Write(datestart, 0, 10);
                    } while (SerialPort1.BytesToRead == 0);
                }
                else if (value == false)
                {
                    do
                    {
                        if (datastop == 1)
                        {
                            stop[0] = 0x55;
                            stop[1] = 0x02;
                            stop[2] = 0x00;
                            stop[3] = 0xAA;
                        }
                        SerialPort1.Write(stop, 0, 4);
                        SerialPort1.DiscardInBuffer();
                        Thread.Sleep(1000); //Delay 1秒
                    } while (SerialPort1.BytesToRead != 0);

                }
            }
        }


        public Boolean OpenStatus
        {
            set
            {
                if (value == true)
                {
                    strRawdatafile = string.Format("Rawdatafile_{0}.txt", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    Rawdatafile = new StreamWriter(strRawdatafile, true);
                    filestatus = true;
                }
                else if (value == false)
                {
                    label_FileStatus.Text = "紀錄完成！可進行下一次紀錄...";
                    Rawdatafile.Close();
                    filestatus = false;
                }

            }
        }

        /* comport */
        private SerialPort CreateComport(SerialPort port, string Comport_connect)
        {
            if (port == null)
            {
                //460800
                port = new SerialPort(Comport_connect, 460800, Parity.None, 8, StopBits.One);
                port.RtsEnable = true;
                port.DtrEnable = true;
            }
            return port;
        }
        private void OpenComport(SerialPort port)
        {
            try
            {

                if (port.IsOpen)
                {
                    port.Close();
                }
                if ((port != null) && (!port.IsOpen))
                {
                    port.Open();
                    record_T = new ThreadStart(Record);
                    record_Thread = new Thread(record_T);
                    record_Thread.Start();
                    getstatus = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("出問題啦:{0}", ex.ToString()));
                Connection = false;
            }
        }
        private void CloseComport(SerialPort port)
        {
            try
            {
                port.Dispose();
                port.Close();
            }
            catch (Exception ex)
            {
                Connection = true;
                MessageBox.Show(String.Format("序列埠出問題啦:{0}", ex.ToString()));
            }
        }

        //畫訊號
        private void draw()
        {
            timerRealTimeData = new System.Windows.Forms.Timer();
            timerRealTimeData.Enabled = true;
            timerRealTimeData.Interval = 10;
            timerRealTimeData.Tick += new System.EventHandler(this.timerRealTimeData_Tick);

        }
        private void timerRealTimeData_Tick(object sender, EventArgs e)
        {
            // Define some variables
            int numberOfPointsInChart = (int)(samplerate * timeshow);
            int numberOfPointsAfterRemoval = (int)(samplerate * timeshow);
            // Simulate adding new data points
            if (drawstatus == true)
            {
                drawstatus = false;
                /*
                if (resultstatus == true)
                { 
                if (eyeblinking == true)
                {
                    label_pattern_energy_ch1.Text = pattern_energy_CH1.ToString();
                    label_pattern_energy_ch2.Text = pattern_energy_CH2.ToString();
                    label_enviroment_noise1_ch1.Text = pattern_environment_CH1.ToString();
                    label_enviroment_noise1_ch2.Text = pattern_environment_CH2.ToString();
                    label_score_text.Text=score.ToString();
                    Mainform.NFT_Pattern_score = score;
                    label_status.Text = "正常量測中";
                    eyeblinking = true;
                }
                else
                {
                    label_pattern_energy_ch1.Text = "眨眼/移動";
                    label_pattern_energy_ch2.Text = "眨眼/移動";


                    label_pattern_energy_ch1.Text = pattern_energy_CH1.ToString();
                    label_pattern_energy_ch2.Text = pattern_energy_CH2.ToString();
                        
                        label_status.Text = "眨眼/移動";
                    label_enviroment_noise1_ch1.Text = pattern_environment_CH1.ToString();
                    label_enviroment_noise1_ch2.Text = pattern_environment_CH2.ToString();
                    label_score_text.Text = score.ToString();
                    Mainform.NFT_Pattern_score = score;
                    eyeblinking = true;
                }
                    resultstatus = false;
               }
               */


                for (int i = 0; i < Newsignalsize; i++)
                {
                    pointIndex = pointIndex + 1;
                    float newY = Draw_CH1[i];
                    float newY2 = Draw_CH2[i];
                    float newY3 = Draw_CH3[i];
                    float newY4 = Draw_CH4[i];
                    chart1.Series[0].Points.AddXY(pointIndex, newY * 1000000);
                    chart2.Series[0].Points.AddXY(pointIndex, newY2 * 1000000);
                    chart3.Series[0].Points.AddXY(pointIndex, newY3 * 1000000);
                    chart4.Series[0].Points.AddXY(pointIndex, newY4 * 1000000);


                    // Adjust Y & X axis scale
                    chart1.ResetAutoValues();
                    if (chart1.ChartAreas["Default"].AxisX.Maximum < pointIndex)
                    {
                        chart1.ChartAreas["Default"].AxisX.Maximum = pointIndex;
                    }
                    // Keep a constant number of points by removing them from the left
                    while (chart1.Series[0].Points.Count > numberOfPointsInChart)
                    {
                        // Remove data points on the left side
                        while (chart1.Series[0].Points.Count > numberOfPointsAfterRemoval)
                        {
                            chart1.Series[0].Points.RemoveAt(0);

                            //chart1.Series[1].Points.RemoveAt(0);
                            //chart1.Series[2].Points.RemoveAt(0);
                            //chart1.Series[3].Points.RemoveAt(0);
                            // chart1.Series[2].Points.RemoveAt(0);
                        }
                        // Adjust X axis scale
                        chart1.ChartAreas["Default"].AxisX.Minimum = pointIndex - numberOfPointsAfterRemoval;
                        chart1.ChartAreas["Default"].AxisX.Maximum = chart1.ChartAreas["Default"].AxisX.Minimum + numberOfPointsInChart;
                    }

                    //Chart 2                    
                    // Adjust Y & X axis scale
                    chart2.ResetAutoValues();
                    if (chart2.ChartAreas["Default"].AxisX.Maximum < pointIndex)
                    {
                        chart2.ChartAreas["Default"].AxisX.Maximum = pointIndex;
                    }
                    // Keep a constant number of points by removing them from the left
                    while (chart2.Series[0].Points.Count > numberOfPointsInChart)
                    {
                        // Remove data points on the left side
                        while (chart2.Series[0].Points.Count > numberOfPointsAfterRemoval)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                        }
                        // Adjust X axis scale
                        chart2.ChartAreas["Default"].AxisX.Minimum = pointIndex - numberOfPointsAfterRemoval;
                        chart2.ChartAreas["Default"].AxisX.Maximum = chart1.ChartAreas["Default"].AxisX.Minimum + numberOfPointsInChart;
                    }
                    //Chart 3                    
                    // Adjust Y & X axis scale
                    chart3.ResetAutoValues();
                    if (chart3.ChartAreas["Default"].AxisX.Maximum < pointIndex)
                    {
                        chart3.ChartAreas["Default"].AxisX.Maximum = pointIndex;
                    }
                    // Keep a constant number of points by removing them from the left
                    while (chart3.Series[0].Points.Count > numberOfPointsInChart)
                    {
                        // Remove data points on the left side
                        while (chart3.Series[0].Points.Count > numberOfPointsAfterRemoval)
                        {
                            chart3.Series[0].Points.RemoveAt(0);
                        }
                        // Adjust X axis scale
                        chart3.ChartAreas["Default"].AxisX.Minimum = pointIndex - numberOfPointsAfterRemoval;
                        chart3.ChartAreas["Default"].AxisX.Maximum = chart1.ChartAreas["Default"].AxisX.Minimum + numberOfPointsInChart;
                    }
                    //Chart 4                    
                    // Adjust Y & X axis scale
                    chart4.ResetAutoValues();
                    if (chart4.ChartAreas["Default"].AxisX.Maximum < pointIndex)
                    {
                        chart4.ChartAreas["Default"].AxisX.Maximum = pointIndex;
                    }
                    // Keep a constant number of points by removing them from the left
                    while (chart4.Series[0].Points.Count > numberOfPointsInChart)
                    {
                        // Remove data points on the left side
                        while (chart4.Series[0].Points.Count > numberOfPointsAfterRemoval)
                        {
                            chart4.Series[0].Points.RemoveAt(0);
                        }
                        // Adjust X axis scale
                        chart4.ChartAreas["Default"].AxisX.Minimum = pointIndex - numberOfPointsAfterRemoval;
                        chart4.ChartAreas["Default"].AxisX.Maximum = chart1.ChartAreas["Default"].AxisX.Minimum + numberOfPointsInChart;
                    }

                    // Redraw chart
                    chart1.Invalidate();
                    chart2.Invalidate();
                    chart3.Invalidate();
                    chart4.Invalidate();









                    /*  if (drawNFTstatus == true)
                      {
                          FFT_chart_list[0].Series[0].Values.Clear();
                          FFT_chart_list[0].Series[1].Values.Clear();
                          for (int m = 1; m <= 26; m++)
                          {
                              FFT_chart_list[0].Series[0].Values.Add((float)FFT_CH1[m]);
                              FFT_chart_list[0].Series[1].Values.Add((float)FFT_CH2[m]);
                          }

                      }*/
                }
                getstatus = true;
            }
        }
        public class RealtimeChart
        {
            private Chart _chart = null;
            private int chartWidth = 50;
            private int chartHeight = 300;
            private string nameAxisX = "Time (s)";
            private string nameAxisY = "Voltage (uV)";

            public RealtimeChart()
            {
                _chart = new Chart();
                ChartArea ctArea = new ChartArea();
                Legend legend = new Legend();
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();


                _chart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(223)))), ((int)(((byte)(193)))));
                _chart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
                _chart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
                _chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                _chart.BorderlineWidth = 2;
                _chart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
                _chart.Location = new System.Drawing.Point(10, 30);
                _chart.Name = "chart1";
                _chart.Size = new System.Drawing.Size(chartWidth, chartHeight);
                _chart.TabIndex = 1;
                _chart.Dock = System.Windows.Forms.DockStyle.Fill;
                // _chart.Dock = System.Windows.Forms.DockStyle.Top;
                ctArea.Area3DStyle.Inclination = 15;
                ctArea.Area3DStyle.IsClustered = true;
                ctArea.Area3DStyle.IsRightAngleAxes = false;
                ctArea.Area3DStyle.Perspective = 10;
                ctArea.Area3DStyle.Rotation = 10;
                ctArea.Area3DStyle.WallWidth = 0;


                ctArea.AxisX.IsLabelAutoFit = false;
                ctArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
                ctArea.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                ctArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                ctArea.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                ctArea.AxisX.Title = nameAxisX;
                ctArea.AxisY.IsLabelAutoFit = false;
                ctArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
                ctArea.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                ctArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                ctArea.AxisY.Maximum = 500D;
                ctArea.AxisY.Minimum = -500D;

                ctArea.AxisY.Title = nameAxisY;
                ctArea.BackColor = System.Drawing.Color.OldLace;
                ctArea.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
                ctArea.BackSecondaryColor = System.Drawing.Color.White;
                ctArea.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(20)))));
                ctArea.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                ctArea.Name = "Default";
                ctArea.ShadowColor = System.Drawing.Color.Transparent;
                _chart.ChartAreas.Add(ctArea);

                legend.BackColor = System.Drawing.Color.Transparent;
                legend.Enabled = false;
                legend.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
                legend.IsTextAutoFit = false;
                legend.Name = "Default";
                _chart.Legends.Add(legend);

                series.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                series.ChartArea = "Default";
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                series.Legend = "Default";
                series.Name = "Default";
                _chart.Series.Add(series);

                series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                series1.ChartArea = "Default";
                series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                series1.Legend = "Default";
                series1.Name = "Default1";
                _chart.Series.Add(series1);


                series2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(152)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                series2.ChartArea = "Default";
                series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                series2.Legend = "Default";
                series2.Name = "Default12";
                _chart.Series.Add(series2);



                series3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                series3.ChartArea = "Default";
                series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                series3.Legend = "Default";
                series3.Name = "Default3";
                _chart.Series.Add(series3);




            }

            public Chart GetChart
            {
                get { return _chart; }
            }
        }
        private void Signalform_Load(object sender, EventArgs e)
        {
            draw();
        }

        //更新CHART 上下界
        private void Button_Set_Click(object sender, EventArgs e)
        {
            double signal_maxnum = Convert.ToDouble(signal_max.Text);
            double signal_minnum = Convert.ToDouble(signal_min.Text);
            double signal2_maxnum = Convert.ToDouble(signal2_max.Text);
            double signal2_minnum = Convert.ToDouble(signal2_min.Text);
            double signal3_maxnum = Convert.ToDouble(signal3_max.Text);
            double signal3_minnum = Convert.ToDouble(signal3_min.Text);
            double signal4_maxnum = Convert.ToDouble(signal4_max.Text);
            double signal4_minnum = Convert.ToDouble(signal4_min.Text);

            /*
            if (signal_maxnum < signal_minnum || signal_maxnum == signal_minnum)
            { 
                signal_maxnum= signal_minnum + 100;
                signal_max.Text = signal_maxnum.ToString();
            }
            if (signal2_maxnum < signal2_minnum || signal2_maxnum == signal2_minnum)
            {
                signal2_maxnum = signal2_minnum + 100;
                signal2_max.Text = signal2_maxnum.ToString();
            }
            if (signal3_maxnum < signal3_minnum || signal3_maxnum == signal3_minnum)
            {
                signal3_maxnum = signal3_minnum + 100;
                signal3_max.Text = signal3_maxnum.ToString();
            }
            if (signal4_maxnum < signal4_minnum || signal4_maxnum == signal4_minnum)
            {
                signal4_maxnum = signal4_minnum + 100;
                signal4_max.Text = signal4_maxnum.ToString();
            }
            */
            chart1.ChartAreas["Default"].AxisY.Maximum = signal_maxnum;
            chart1.ChartAreas["Default"].AxisY.Minimum = signal_minnum;
            chart2.ChartAreas["Default"].AxisY.Maximum = signal2_maxnum;
            chart2.ChartAreas["Default"].AxisY.Minimum = signal2_minnum;
            chart3.ChartAreas["Default"].AxisY.Maximum = signal3_maxnum;
            chart3.ChartAreas["Default"].AxisY.Minimum = signal3_minnum;
            chart4.ChartAreas["Default"].AxisY.Maximum = signal4_maxnum;
            chart4.ChartAreas["Default"].AxisY.Minimum = signal4_minnum;

        }

        /* Receive Signal */
        private void Record()
        {

            float[,] Voltage = new float[Channel, Datapoint];
            while (true)
            {
                if (getstatus == true)
                {
                    if (SerialPort1.BytesToRead >= ReadBuffer.Length)
                    {
                        SerialPort1.Read(ReadBuffer, 0, ReadBuffer.Length);
                        //Console.WriteLine(ReadBuffer);
                        //使用哪個DataConvert
                        //Voltage = DataConvert_152(ReadBuffer);
                        Voltage = DataConvert_232(ReadBuffer);
                        for (int i = 0; i < Datapoint; i++)
                        {
                            Temp_CH1[count] = Voltage[0, i];
                            Temp_CH2[count] = Voltage[1, i];
                            Temp_CH3[count] = Voltage[2, i];
                            Temp_CH4[count] = Voltage[3, i];
                            count = count + 1;
                            Datapoint_count = Datapoint_count + 1;
                        }
                        if (filestatus == true)
                        {
                            for (int i = 0; i < ReadBuffer.Length; i++)
                            {
                                Rawdatafile.WriteLine(ReadBuffer[i]);
                                // Console.WriteLine(ReadBuffer[i]);
                            }

                        }
                    }

                    if (count >= Newsignalsize)
                    {
                        count = count - Newsignalsize;


                        if (Datapoint_count > signalfiltersize)
                        {

                            // Buffer.BlockCopy(src, 5, dest, 7, 6 )  將src陣列地5位置開始(含)六個(src[5] ~ src[10])複製到dest從第7位置開始((dest[7] ~ dest[12]))
                            Buffer.BlockCopy(Data_CH1, 4 * Newsignalsize, Data_CH1, 0, 4 * moveignalsize); // float  = 4 byte
                            Buffer.BlockCopy(Temp_CH1, 0, Data_CH1, 4 * moveignalsize, 4 * Newsignalsize); // float  = 4 byte
                                                                                                           //Draw_CH1 = filter.bandpassfilter(Data_CH1);
                            Draw_CH1 = filter.Sportbandpassfilter(Data_CH1);
                            nftpoint += 130;
                            // Buffer.BlockCopy(Draw_CH1, 0, NFTBuffer_ch1, 4 * NFTpoint_count, 4 * Newsignalsize); // float  = 4 byte
                            Buffer.BlockCopy(Draw_CH1, 0, NFTBuffer_ch1, 4 * 0, 4 * 130);


                            Buffer.BlockCopy(Data_CH2, 4 * Newsignalsize, Data_CH2, 0, 4 * moveignalsize); // float  = 4 byte
                            Buffer.BlockCopy(Temp_CH2, 0, Data_CH2, 4 * moveignalsize, 4 * Newsignalsize); // float  = 4 byte
                                                                                                           //Draw_CH2 = filter.bandpassfilter(Data_CH2);
                            Draw_CH2 = filter.Sportbandpassfilter(Data_CH2);
                            Buffer.BlockCopy(Draw_CH2, 0, NFTBuffer_ch2, 4 * 0, 4 * 130);



                            Buffer.BlockCopy(Data_CH3, 4 * Newsignalsize, Data_CH3, 0, 4 * moveignalsize); // float  = 4 byte
                            Buffer.BlockCopy(Temp_CH3, 0, Data_CH3, 4 * moveignalsize, 4 * Newsignalsize); // float  = 4 byte
                                                                                                           // Draw_CH3 = filter.bandpassfilter(Data_CH3);
                            Draw_CH3 = filter.Sportbandpassfilter(Data_CH3);




                            Buffer.BlockCopy(Data_CH4, 4 * Newsignalsize, Data_CH4, 0, 4 * moveignalsize); // float  = 4 byte
                            Buffer.BlockCopy(Temp_CH4, 0, Data_CH4, 4 * moveignalsize, 4 * Newsignalsize); // float  = 4 byte
                                                                                                           //Draw_CH4 = filter.bandpassfilter(Data_CH4);
                            Draw_CH4 = filter.Sportbandpassfilter(Data_CH4);



                            // Buffer.BlockCopy(Draw_CH2, 0, NFTBuffer_ch2, 4 * NFTpoint_count, 4 * Newsignalsize); // float  = 4 byte

                            NFTpoint_count = NFTpoint_count + Newsignalsize;


                            if ((NFTpoint_count / 250) >= 1)
                            {
                                if (filestatus == true)
                                {
                                    MethodInvoker mi = new MethodInvoker(this.update_time);
                                    this.BeginInvoke(mi, null);
                                }

                                Buffer.BlockCopy(NFTBuffer_ch1, 0, NFTcal_ch1, 0, 4 * 250); // float  = 4 byte
                                Buffer.BlockCopy(NFTBuffer_ch2, 0, NFTcal_ch2, 0, 4 * 250); // float  = 4 byte 

                                FFT_CH1 = PlotFFT(NFTcal_ch1);
                                FFT_CH2 = PlotFFT(NFTcal_ch2);
                     

                                //// 設定檔案名稱，取得桌面路徑
                                //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                //string folderName = "HOYI";
                                //string folderPath = Path.Combine(desktopPath, folderName);

                                //if (!Directory.Exists(folderPath))
                                //{
                                //    Directory.CreateDirectory(folderPath);
                                //}

                                //string fileName = "FFT_Data_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                                //string filePath = Path.Combine(folderPath, fileName);

                                //if (!Directory.Exists(folderPath))
                                //{
                                //    Directory.CreateDirectory(folderPath);
                                //}

                                //// 打開文件並寫入接收到的數據
                                //using (StreamWriter writer = new StreamWriter(filePath, true))
                                //{
                                //    for (int i = 0; i < FFT_CH1.Length; i++)
                                //    {
                                //        writer.WriteLine(FFT_CH1[i]);
                                //    }
                                //}

                                frequency_chart.Series[0].Values.Clear();
                                frequency_chart.Series[1].Values.Clear();
                                frequency_chart.Series[0].Values.Add(0.0);
                                frequency_chart.Series[1].Values.Add(0.0);

                                for (int fft_index = 1; fft_index <= 25; fft_index++)
                                {
                                    frequency_chart.Series[0].Values.Add(FFT_CH1[fft_index]);
                                    frequency_chart.Series[1].Values.Add(FFT_CH2[fft_index]);
                                }

                                Buffer.BlockCopy(NFTBuffer_ch1, 4 * 250, NFTBuffer_ch1, 0, 4 * (NFTpoint_count - 250)); // float  = 4 byte
                                Buffer.BlockCopy(NFTBuffer_ch2, 4 * 250, NFTBuffer_ch2, 0, 4 * (NFTpoint_count - 250)); // float  = 4 byte 

                                NFTpoint_count = NFTpoint_count - 250;

                                ////// 設定檔案名稱，取得桌面路徑
                                //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                //string fileName = "FFT_Data_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                                //string filePath = Path.Combine(desktopPath, fileName);

                                //// 打開文件並寫入接收到的數據
                                //using (StreamWriter writer = new StreamWriter(filePath, true))
                                //{
                                //    for (int i = 0; i < FFT_CH1.Length; i++)
                                //    {
                                //        writer.WriteLine(FFT_CH1[i]);
                                //    }
                                //}
                                                                                                   
                                if (filestatus == true)
                                {
                                    // 設定檔案名稱，取得桌面路徑
                                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                    string folderName1 = "immediate_data/CH1";
                                    string folderPath1 = Path.Combine(desktopPath, folderName1);

                                    if (!Directory.Exists(folderPath1))
                                    {
                                        Directory.CreateDirectory(folderPath1);
                                    }

                                    string fileName = "FFT_Data_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                                    string filePath1 = Path.Combine(folderPath1, fileName);

                                    if (!Directory.Exists(folderPath1))
                                    {
                                        Directory.CreateDirectory(folderPath1);
                                    }

                                    // 打開文件並寫入接收到的數據
                                    using (StreamWriter writer = new StreamWriter(filePath1, true))
                                    {
                                        for (int i = 0; i < FFT_CH1.Length; i++)
                                        {
                                            writer.WriteLine(FFT_CH1[i] * 1000000);
                                            Console.WriteLine(FFT_CH1[i] * 1000000);
                                        }
                                    }

                                    ///////////////////////////////////////////////////////////////////
                                    string folderName2 = "immediate_data/CH2";
                                    string folderPath2 = Path.Combine(desktopPath, folderName2);

                                    if (!Directory.Exists(folderPath2))
                                    {
                                        Directory.CreateDirectory(folderPath2);
                                    }

                                    string filePath2 = Path.Combine(folderPath2, fileName);

                                    if (!Directory.Exists(folderPath2))
                                    {
                                        Directory.CreateDirectory(folderPath2);
                                    }

                                    // 打開文件並寫入接收到的數據
                                    using (StreamWriter writer = new StreamWriter(filePath2, true))
                                    {
                                        for (int ii = 0; ii < FFT_CH2.Length; ii++)
                                        {
                                            writer.WriteLine(FFT_CH2[ii] * 1000000);
                                            Console.WriteLine(FFT_CH2[ii] * 1000000);
                                        }
                                    }
                                    /////////////////////////////////////////////////////////////////
                                    
                                    ///////////////////////////////////////////////////////////////////
                                    //string folderName3 = "immediate_data/CH3";
                                    //string folderPath3 = Path.Combine(desktopPath, folderName3);

                                    //if (!Directory.Exists(folderPath3))
                                    //{
                                    //    Directory.CreateDirectory(folderPath3);
                                    //}

                                    //string filePath3 = Path.Combine(folderPath3, fileName);

                                    //if (!Directory.Exists(folderPath3))
                                    //{
                                    //    Directory.CreateDirectory(folderPath3);
                                    //}

                                    //// 打開文件並寫入接收到的數據
                                    //using (StreamWriter writer = new StreamWriter(filePath3, true))
                                    //{
                                    //    for (int iii = 0; iii < FFT_CH3.Length; iii++)
                                    //    {
                                    //        writer.WriteLine(FFT_CH3[iii] * 1000000);
                                    //        Console.WriteLine(FFT_CH3[iii] * 1000000);
                                    //    }
                                    //}
                                    /////////////////////////////////////////////////////////////////


                                    for (int i = 0; i < 250; i++)
                                    {
                          

                                        if (NFTcal_ch1[i] * 1000000 > 70 || NFTcal_ch2[i] * 1000000 > 70 || NFTcal_ch1[i] * 1000000 < (-70) || NFTcal_ch2[i] * 1000000 < (-70))
                                        {
                                            eyeblinking = false;
                                            break;
                                        }
                                    }
                                    if (eyeblinking == false) // 有眨眼分數10分
                                    {
                                        pattern_energy_CH1 = 0;
                                        pattern_energy_CH2 = 0;
                                        score = 10; // min score
                                        eyeblinking = true;
                                        blink_times++;
                                    }
                                    else // 無眨眼
                                    {
                                        //計算alpha　pattern　大小
                                        pattern_energy_CH1 = (float)((FFT_CH1[8] + FFT_CH1[9] + FFT_CH1[10] + FFT_CH1[11] + FFT_CH1[7]));
                                        pattern_energy_CH2 = (float)((FFT_CH2[8] + FFT_CH2[9] + FFT_CH2[10] + FFT_CH2[11] + FFT_CH2[7]));

                                        NFT_pattern_energy = ((pattern_energy_CH1) + (pattern_energy_CH2)) * 1000000 / 2;
                                        if (NFT_pattern_energy > 0 && NFT_pattern_energy < 0.001) // 未配戴裝置
                                        {
                                            score = 0;
                                            error_times++;
                                        }
                                        else if (NFT_pattern_energy > 0 && NFT_pattern_energy <= 7.5)
                                        {
                                            score = 10 + (int)((NFT_pattern_energy - 0) * 40 / 7.5);
                                            total_score += score;
                                        }
                                        else if (NFT_pattern_energy > 7.5 && NFT_pattern_energy <= 15)
                                        {
                                            score = 50 + (int)((NFT_pattern_energy - 7.5) * 40 / 7.5);
                                            total_score += score;
                                        }
                                        else if (NFT_pattern_energy > 15 && NFT_pattern_energy <= 37.5)
                                        {
                                            score = 90 + (int)((NFT_pattern_energy - 15) * 8 / 22.5);
                                            total_score += score;
                                        }
                                        else
                                        {
                                            score = 98;
                                            total_score += score;
                                        }
                                    }
                                    
                                    if (filestatus == true)
                                    {
                                        MethodInvoker mi2 = new MethodInvoker(this.update_progress);
                                        this.BeginInvoke(mi2, null);
                                    }
                                }
                                

                                /*

                                //將結果傳回介面(old version)
                                NFT_pattern_energy = ((pattern_energy_CH1) + (pattern_energy_CH2)) * 1000000 / 2;
                                if (NFT_pattern_energy > 0 && NFT_pattern_energy <= 3)
                                    score = 10 + (float)((NFT_pattern_energy - 0) * 50 / 3);
                                else if (NFT_pattern_energy >= 3 && NFT_pattern_energy <= 6)
                                    score = 50 + (float)((NFT_pattern_energy - 3) * 40 / 3);
                                else if (NFT_pattern_energy >= 6 && NFT_pattern_energy <= 15)
                                    score = 90 + (float)((NFT_pattern_energy - 6) * 8 / 9);
                                else if (NFT_pattern_energy >= 15)
                                    score = 98;

                                */

                                //Console.WriteLine("%d\n", score);
                                

                                //drawNFTstatus = true;
                            }
                            getstatus = false;
                            drawstatus = true;
                        }
                        else
                        {
                            int temp = Datapoint_count - Newsignalsize;
                            Buffer.BlockCopy(Temp_CH1, 0, Data_CH1, 4 * temp, 4 * Newsignalsize); // float  = 4 byte
                            Buffer.BlockCopy(Temp_CH2, 0, Data_CH2, 4 * temp, 4 * Newsignalsize); // float  = 4 byte
                            Buffer.BlockCopy(Temp_CH3, 0, Data_CH3, 4 * temp, 4 * Newsignalsize); // float  = 4 byte
                            Buffer.BlockCopy(Temp_CH4, 0, Data_CH4, 4 * temp, 4 * Newsignalsize); // float  = 4 byte
                        }
                    }
                }
            }
        }
        private float[,] DataConvert_152(byte[] bufferdata) // 152 byte 轉換
        {
            float[,] Outputsig = new float[Channel, Datapoint];
            byte[] PackageData = new byte[Packagesize];
            for (int i = 0; i < ReadBuffer.Length; i++)
            {
                if (bufferdata[i] == 173 &&
                    bufferdata[i + 1] == 222 &&
                    bufferdata[i + Packagesize - Endbytessize] == 239 &&
                    bufferdata[i + Packagesize - Endbytessize + 1] == 190)
                {
                    Buffer.BlockCopy(ReadBuffer, i, PackageData, 0, Packagesize);
                    // find LSB miss package ex. [y0][y1][239][190][173][222] -> bufferMiss = [y0][y1][239][190] 
                    if (i < Packagesize)
                    {
                        Buffer.BlockCopy(ReadBuffer, 0, BufferMiss, BufferMiss.Length - i, i);
                        // convert missing data
                        if (BufferMiss[0] == 173 &&
                            BufferMiss[1] == 222 &&
                            BufferMiss[Packagesize - Endbytessize] == 239 &&
                            BufferMiss[Packagesize - Endbytessize + 1] == 190)
                        {
                            Outputsig = DataConvert_18points(BufferMiss);
                            Array.Clear(BufferMiss, 0, BufferMiss.Length);
                        }
                    }
                    i += (Packagesize - 1);
                    Outputsig = DataConvert_18points(PackageData);
                }
                // find MSB miss package ex. [173][222][x0][x1]...[239][190] -> bufferMiss = [173][222][x0][x1]...[]
                if (i >= bufferdata.Length - Packagesize)
                {
                    Buffer.BlockCopy(ReadBuffer, i + 1, BufferMiss, 0, (bufferdata.Length - i - 1));
                    break;
                }
            }
            return Outputsig;
        }

        private float[,] DataConvert_232(byte[] bufferdata) // 152 byte 轉換
        {
            float[,] Outputsig = new float[Channel, Datapoint];
            byte[] PackageData = new byte[Packagesize];
            for (int i = 0; i < ReadBuffer.Length; i++)
            {
                if (bufferdata[i] == 173 &&
                    bufferdata[i + 1] == 222 &&
                    bufferdata[i + Packagesize - Endbytessize] == 239 &&
                    bufferdata[i + Packagesize - Endbytessize + 1] == 190)
                {
                    Buffer.BlockCopy(ReadBuffer, i, PackageData, 0, Packagesize);
                    // find LSB miss package ex. [y0][y1][239][190][173][222] -> bufferMiss = [y0][y1][239][190] 
                    if (i < Packagesize)
                    {
                        Buffer.BlockCopy(ReadBuffer, 0, BufferMiss, BufferMiss.Length - i, i);
                        // convert missing data
                        if (BufferMiss[0] == 173 &&
                            BufferMiss[1] == 222 &&
                            BufferMiss[Packagesize - Endbytessize] == 239 &&
                            BufferMiss[Packagesize - Endbytessize + 1] == 190)
                        {
                            Outputsig = DataConvert_13points(BufferMiss);
                            Array.Clear(BufferMiss, 0, BufferMiss.Length);
                        }
                    }
                    i += (Packagesize - 1);
                    Outputsig = DataConvert_13points(PackageData);
                }
                // find MSB miss package ex. [173][222][x0][x1]...[239][190] -> bufferMiss = [173][222][x0][x1]...[]
                if (i >= bufferdata.Length - Packagesize)
                {
                    Buffer.BlockCopy(ReadBuffer, i + 1, BufferMiss, 0, (bufferdata.Length - i - 1));
                    break;
                }
            }
            return Outputsig;
        }


        private float[,] DataConvert_13points(byte[] packdata) // (18個點* 2)
        {
            float[,] outsignal = new float[Channel, Datapoint];
            //  float[,] outsignal = new float[2, 18] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            // byte[,] CH1_Signal_value = new byte[4, 13] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            // byte[,] CH1_Signal_value = new byte[Point_size, Datapoint];
            //byte[,] CH2_Signal_value = new byte[4, 18] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            byte[,] CH1_Signal_value = new byte[Point_size, Datapoint];
            byte[,] CH2_Signal_value = new byte[Point_size, Datapoint];
            byte[,] CH3_Signal_value = new byte[Point_size, Datapoint];
            byte[,] CH4_Signal_value = new byte[Point_size, Datapoint];
            byte[] temp = new byte[3];
            float TempVoltage = 0;
            string ms;
            int raw = 0;
            int column = 0;
            //channel_1
            for (int j = Startbytessize; j < Startbytessize + Datapoint * Point_size; j++) //每一封包的第4~76 BYTES 為CH1 的DATA
            {
                CH1_Signal_value[raw, column] = packdata[j];
                raw = raw + 1;
                if (raw == 4)
                {
                    raw = 0;
                    column = column + 1;
                }
            }
            for (int i = 0; i < Datapoint; i++)
            {
                temp[0] = CH1_Signal_value[2, i];
                temp[1] = CH1_Signal_value[1, i];
                temp[2] = CH1_Signal_value[0, i];
                ms = BitConverter.ToString(temp);
                //移除全部的“-”：
                ms = ms.Replace("-", "").Trim();
                //最後轉化成標,準十進制
                TempVoltage = Convert.ToInt32(ms, 16);
                //負數翻轉
                if (TempVoltage >= 10000000)
                    TempVoltage = TempVoltage - 16777216;
                //CH1_output = (data2_CH1)*(4.5) /gain / (2^23);
                TempVoltage = TempVoltage * 9 / 48 / 8388608;
                outsignal[0, i] = TempVoltage;
            }

            column = 0;
            raw = 0;
            //channel_2
            for (int j = Startbytessize + Datapoint * Point_size; j < Startbytessize + Datapoint * Point_size * 2; j++) //每一封包的第76~148 BYTES 為CH1 的DATA
            {
                CH2_Signal_value[raw, column] = packdata[j];
                raw = raw + 1;
                if (raw == 4)
                {
                    raw = 0;
                    column = column + 1;
                }
            }
            for (int i = 0; i < Datapoint; i++)
            {
                temp[0] = CH2_Signal_value[2, i];
                temp[1] = CH2_Signal_value[1, i];
                temp[2] = CH2_Signal_value[0, i];
                ms = BitConverter.ToString(temp);
                //移除全部的“-”：
                ms = ms.Replace("-", "").Trim();
                //最後轉化成標,準十進制
                TempVoltage = Convert.ToInt32(ms, 16);
                //負數翻轉
                if (TempVoltage >= 10000000)
                    TempVoltage = TempVoltage - 16777216;
                //CH1_output = (data2_CH1)*(4.5) /gain / (2^23);
                TempVoltage = TempVoltage * 9 / 48 / 8388608;
                outsignal[1, i] = TempVoltage;
            }

            column = 0;
            raw = 0;
            //channel_3
            for (int j = Startbytessize + Datapoint * Point_size * 2; j < Startbytessize + Datapoint * Point_size * 3; j++) //每一封包的第76~148 BYTES 為CH1 的DATA
            {
                CH3_Signal_value[raw, column] = packdata[j];
                raw = raw + 1;
                if (raw == 4)
                {
                    raw = 0;
                    column = column + 1;
                }
            }
            for (int i = 0; i < Datapoint; i++)
            {
                temp[0] = CH3_Signal_value[2, i];
                temp[1] = CH3_Signal_value[1, i];
                temp[2] = CH3_Signal_value[0, i];
                ms = BitConverter.ToString(temp);
                //移除全部的“-”：
                ms = ms.Replace("-", "").Trim();
                //最後轉化成標,準十進制
                TempVoltage = Convert.ToInt32(ms, 16);
                //負數翻轉
                if (TempVoltage >= 10000000)
                    TempVoltage = TempVoltage - 16777216;
                //CH1_output = (data2_CH1)*(4.5) /gain / (2^23);
                TempVoltage = TempVoltage * 9 / 48 / 8388608;
                outsignal[2, i] = TempVoltage;
            }


            column = 0;
            raw = 0;
            //channel_4
            for (int j = Startbytessize + Datapoint * Point_size * 3; j < Startbytessize + Datapoint * Point_size * 4; j++) //每一封包的第76~148 BYTES 為CH1 的DATA
            {
                CH4_Signal_value[raw, column] = packdata[j];
                raw = raw + 1;
                if (raw == 4)
                {
                    raw = 0;
                    column = column + 1;
                }
            }
            for (int i = 0; i < Datapoint; i++)
            {
                temp[0] = CH4_Signal_value[2, i];
                temp[1] = CH4_Signal_value[1, i];
                temp[2] = CH4_Signal_value[0, i];
                ms = BitConverter.ToString(temp);
                //移除全部的“-”：
                ms = ms.Replace("-", "").Trim();
                //最後轉化成標,準十進制
                TempVoltage = Convert.ToInt32(ms, 16);
                //負數翻轉
                if (TempVoltage >= 10000000)
                    TempVoltage = TempVoltage - 16777216;
                //CH1_output = (data2_CH1)*(4.5) /gain / (2^23);
                TempVoltage = TempVoltage * 9 / 48 / 8388608;
                outsignal[3, i] = TempVoltage;
            }


            return outsignal;
        }


        private float[,] DataConvert_18points(byte[] packdata) // (18個點* 2)
        {
            float[,] outsignal = new float[Channel, Datapoint];
            //  float[,] outsignal = new float[2, 18] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            byte[,] CH1_Signal_value = new byte[4, 18] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            // byte[,] CH1_Signal_value = new byte[Point_size, Datapoint];
            //byte[,] CH2_Signal_value = new byte[4, 18] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

            byte[,] CH2_Signal_value = new byte[Point_size, Datapoint];
            byte[] temp = new byte[3];
            float TempVoltage = 0;
            string ms;
            int raw = 0;
            int column = 0;
            //channel_1
            for (int j = Startbytessize; j < Startbytessize + Datapoint * Point_size; j++) //每一封包的第4~76 BYTES 為CH1 的DATA
            {
                CH1_Signal_value[raw, column] = packdata[j];
                raw = raw + 1;
                if (raw == 4)
                {
                    raw = 0;
                    column = column + 1;
                }
            }
            for (int i = 0; i < Datapoint; i++)
            {
                temp[0] = CH1_Signal_value[2, i];
                temp[1] = CH1_Signal_value[1, i];
                temp[2] = CH1_Signal_value[0, i];
                ms = BitConverter.ToString(temp);
                //移除全部的“-”：
                ms = ms.Replace("-", "").Trim();
                //最後轉化成標,準十進制
                TempVoltage = Convert.ToInt32(ms, 16);
                //負數翻轉
                if (TempVoltage >= 10000000)
                    TempVoltage = TempVoltage - 16777216;
                //CH1_output = (data2_CH1)*(4.5) /gain / (2^23);
                TempVoltage = TempVoltage * 9 / 48 / 8388608;
                outsignal[0, i] = TempVoltage;
            }
            column = 0;
            raw = 0;
            //channel_2
            for (int j = Startbytessize + Datapoint * Point_size; j < Packagesize - Endbytessize; j++) //每一封包的第76~148 BYTES 為CH1 的DATA
            {
                CH2_Signal_value[raw, column] = packdata[j];
                raw = raw + 1;
                if (raw == 4)
                {
                    raw = 0;
                    column = column + 1;
                }
            }
            for (int i = 0; i < Datapoint; i++)
            {
                temp[0] = CH2_Signal_value[2, i];
                temp[1] = CH2_Signal_value[1, i];
                temp[2] = CH2_Signal_value[0, i];
                ms = BitConverter.ToString(temp);
                //移除全部的“-”：
                ms = ms.Replace("-", "").Trim();
                //最後轉化成標,準十進制
                TempVoltage = Convert.ToInt32(ms, 16);
                //負數翻轉
                if (TempVoltage >= 10000000)
                    TempVoltage = TempVoltage - 16777216;
                //CH1_output = (data2_CH1)*(4.5) /gain / (2^23);
                TempVoltage = TempVoltage * 9 / 48 / 8388608;
                outsignal[1, i] = TempVoltage;
            }
            return outsignal;
        }

        /* FFT */
        public double[] PlotFFT(float[] data)
        {
            int numsample = data.Length;
            double[] msg = new double[numsample / 2];
            //strPicFiledataFFT = string.Format("{0}_ch1.txt", DateTime.Now.ToString("yyyyMMddhhmmss")); // File Name
            //DATASAVE = new StreamWriter(strPicFiledataFFT, true);
            Complex[] samples = new Complex[250];
            for (int i = 0; i < numsample; i++)
            {
                //Console.WriteLine(data[i] * 1000000);
                samples[i] = new Complex(data[i], 0);
                // samples[i] = new Complex(fnudmental[i]+second[i], 0);
            }
            //"Forward" Fourier convert time => Frequency
            // Fourier.Forward(samples, FourierOptions.NoScaling);
            Fourier.Forward(samples, FourierOptions.NoScaling);

            //plot the 頻率 spectrum,SINCE it is "bidirectional bandwidth"

            for (int i = 0; i < numsample / 2; i++)
            {
                try
                {
                    /*double REALTEMP = (float)Math.Pow((float)samples[i].Real, 2);
                    REALTEMP = Math.Sqrt((float)REALTEMP);
                    double ImaginaryTEMP = (float)Math.Pow(samples[i].Imaginary, 2);
                    msg[i] = ((2.0 / numsample) * (Math.Abs(REALTEMP + ImaginaryTEMP)));*/
                    msg[i] = (Math.Abs(Math.Sqrt(Math.Pow(samples[i].Real, 2) + Math.Pow(samples[i].Imaginary, 2))));
                    //Console.WriteLine(samples[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("發生例外狀況：{0}", e.Message);
                }

            }

            return msg;
        }

        private void label_enviroment_noise1_Click(object sender, EventArgs e)
        {

        }

        private void label_enviroment_noise2_Click(object sender, EventArgs e)
        {

        }



        private void BTN_search_Click(object sender, EventArgs e)
        {
            comport_search();
        }
        void comport_search()
        {
            //掃描 dongle 的 comport
            comboBox_Comport.Items.Clear();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
            {
                //使用ManagementObjectSearcher來查詢註冊表中的裝置名稱
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();//取得所有ManagementBaseObject並轉成List
                string[] PortsName = new string[ports.Count];
                Portscom = new string[ports.Count];
                for (int i = 0; i < ports.Count; i++)
                {
                    Portscom[i] = ports[i]["DeviceID"] as string;
                    PortsName[i] = ports[i]["Caption"] as string;//取得裝置名稱與連接埠
                    if (PortsName[i].IndexOf("Silicon Labs CP210x USB to UART Bridge") > -1)
                    {
                        PortsName[i] = "Dongle(" + Portscom[i] + ")";
                    }
                }
                comboBox_Comport.Text = "";
                string[] serialPorts = SerialPort.GetPortNames();
                for (int i = 0; i < ports.Count; i++)
                {
                    comboBox_Comport.Items.Add(PortsName[i]);
                    if (comboBox_Comport.Items.Count > 0)
                    {
                        comboBox_Comport.SelectedIndex = 0;
                    }
                }
            }
        }



        public void BTN_Connect_Click(object sender, EventArgs e)
        {
            Usercom = Portscom[this.comboBox_Comport.SelectedIndex];
            comport_connection = true;
            round_status = true;
            Connection = true;
            Status = true;

        }
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            OpenStatus = true;
            // trigger gopro
            string gopro_start = "http://10.5.5.9/gp/gpControl/command/shutter?p=1";
            HttpWebRequest request = HttpWebRequest.Create(gopro_start) as HttpWebRequest;
            request.Method = "GET";
            request.Timeout = 3000;
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36";
            string result = "";
            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            //    {
            //        result = sr.ReadToEnd();
            //    }
            //}
            //Console.WriteLine(result);
            label_FileStatus.Text = "紀錄中...";
            train_time = int.Parse(textBox1.Text);
            feedback_form.clear_line();
        }

        private void BTN_Stop_Click(object sender, EventArgs e)
        {
            OpenStatus = false;
            label_FileStatus.Text = "紀錄完成！可進行下一次紀錄...";
        }

        private void Signalform_FormClosed(object sender, FormClosedEventArgs e)
        {
            comport_connection = false;
            round_status = false;
            Status = true;
            Connection = true;
            this.Close();
        }

        private void BTN_dvStart_Click(object sender, EventArgs e)
        {
            round_status = true;
            Status = true;
        }

        private void BTN_dvstop_Click(object sender, EventArgs e)
        {
            round_status = false;
            Status = false;
        }

        private void BTN_Reconnect_Click(object sender, EventArgs e)
        {
            comport_connection = false;
            Connection = true;
            //SerialPort1.Dispose();
            //SerialPort1.Close();
            label_status.Text = "COM Status:已斷開，請重新連線";
            //會進到record
        }

        private void materialLabel4_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void update_progress()
        {
            Console.WriteLine(score);
            //progressBar1.Value = (int)score;
            feedback_form.update_bar((int)score);
            feedback_form.update_line((int)score, system_time);
        }

        private void update_time()
        {
            if(system_time >= train_time)
            {
                OpenStatus = false;
                // trigger gopro
                string gopro_start = "http://10.5.5.9/gp/gpControl/command/shutter?p=0";
                HttpWebRequest request = HttpWebRequest.Create(gopro_start) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36";
                request.Timeout = 3000;
                string result = "";
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                Console.WriteLine(result);
                system_time = 0;
                feedback_form.update_time_lab(system_time);
                update_status("訓練結束，待機中");
                if (error_times+blink_times >= (train_time / 3))
                {
                    MessageBox.Show("訊號異常，訓練失敗，請再訓練一次");
                }
                else
                {
                    MessageBox.Show("該次訓練分數為：" + (total_score/(train_time-error_times-blink_times)).ToString());
                }
                error_times = 0;
                blink_times = 0;
                total_score = 0;
            }
            else
            {
                system_time++;
                feedback_form.update_time_lab(system_time);
                update_status("訓練中");
            }
        }

        private void update_status(string status)
        {
            feedback_form.update_status_lab(status);
        }
    }
}
