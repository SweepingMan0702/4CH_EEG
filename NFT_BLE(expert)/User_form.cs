using LiveCharts;
using LiveCharts.Wpf;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;

namespace NFT_BLE_expert_
{
    public partial class User_form : MaterialForm
    {


        /* user information */
        string Username = "";
        string Usercom;
        public Signalform User_signalform = new Signalform();


        /* COMPORT */
        Boolean Connection = false;
        string[] Portscom;

        /*score*/
        float [] Score = new float[361];//畫圖(1~360第一秒至360秒)
        float[] Round = new float[100];//畫圖(1~360第一秒至360秒)


        float NFT_Pattern_energy_score = 0;

        //狀態
        Boolean status = false;
        Boolean trainingstatus = false;
        Boolean openfilestatus = false;

        //BAR progrss 自己用才可換顏色   
        double pbunit;
        int pbwidth, pbhight,pbcomplete;
        Bitmap bmp ;
        Graphics g;
        int count = 0;


        // 以下List 裡為int 型態
        List<LiveCharts.WinForms.CartesianChart> chart_list = new List<LiveCharts.WinForms.CartesianChart>();
        List<LiveCharts.WinForms.CartesianChart> Roundchart_list = new List<LiveCharts.WinForms.CartesianChart>();


        int  Timeserial= 0;
        int  SessionTime = 360;
        int roundcount = 0;


        public User_form()
        {
            InitializeComponent();
            //美化介面
            MaterialSkin.MaterialSkinManager manager = MaterialSkin.MaterialSkinManager.Instance;
            manager.AddFormToManage(this);
            manager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Blue800, MaterialSkin.Primary.Blue900, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.LightBlue400, MaterialSkin.TextShade.WHITE);
            comport_search();
            User_signalform.Show();
            User_signalform.Mainform = this;
            pbwidth = progress_score.Width;
            pbhight = progress_score.Height;
            pbcomplete = 0;
            pbunit = pbwidth / 100.0;
            bmp=new Bitmap(pbwidth, pbhight);
            g = Graphics.FromImage(bmp);

            //為了畫圖正常
            Score[0] = 0;

        }

        //nft result (從計算葉面接收值)
        /*
        public float NFT_Pattern_score
        {
            get { return NFT_Pattern_energy_score; }
            set
            {
                NFT_Pattern_energy_score = value;
                if (trainingstatus)
                {
                    g.Clear(Color.LightCyan);
                    progress_score.Image = bmp;
                    Timeserial = Timeserial+1;
                    count = count + 1;
                    if (count <=10)
                    {
                        label_hint.Text = (10 - count).ToString() + "秒後開始訓練";
                        g.FillRectangle(Brushes.Pink, new Rectangle(0, 0, (int)((10 - count)*10 * pbunit), pbhight));
                    }
                    else if (count > 10 && Timeserial <= SessionTime)
                    {
                        Score[Timeserial] = NFT_Pattern_energy_score;
                        label_hint.Text = Timeserial.ToString() + "秒";
                        if (NFT_Pattern_energy_score >= 0)
                        {
                            g.FillRectangle(Brushes.Pink, new Rectangle(0, 0, (int)(NFT_Pattern_energy_score * pbunit), pbhight));
                            chart_list[0].Series[0].Values.Add((float)(NFT_Pattern_energy_score));
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Pink, new Rectangle(0, 0, (int)(10 * pbunit), pbhight));
                        }

                    }
                    else
                    {
                        status = true;
                        User_signalform.Status = status;
                        label_hint.Text = "結束";
                    }
                }
            }

        }
        */

        //介面關閉
        private void Form1_Closed(object sender, System.EventArgs e)
        {
            Dispose();
            this.Close();
        }

        private void Button_comport_search_Click(object sender, EventArgs e)
        {
            comport_search();
        }


         void comport_search()
        {
            //掃描 dongle 的 comport
            comboBox_port.Items.Clear();
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
                comboBox_port.Text = "";
                string[] serialPorts = SerialPort.GetPortNames();
                for (int i = 0; i < ports.Count; i++)
                {
                    comboBox_port.Items.Add(PortsName[i]);
                    if (comboBox_port.Items.Count > 0)
                    {
                        comboBox_port.SelectedIndex = 0;
                    }
                }
            }
        }

        private void Button_login_Click(object sender, EventArgs e)
        {
            Usercom = Portscom[this.comboBox_port.SelectedIndex];
            User_signalform.Usercom = Usercom;
            Username = Textbox_username.Text;
         //   User_signalform.Username = Username;
            Connection = true;
            User_signalform.Connection = Connection;
            status = true;
            User_signalform.Status = status;
        }

        private void Button_finish_Click(object sender, EventArgs e)
        {
            
            Connection = false;
            User_signalform.Connection = Connection;
            // this.Close();
            Chart_score_round.Series.Clear();
            // 宣告myIntLists 為List
            // 以下List 裡為int 型態
            List<LiveCharts.WinForms.CartesianChart> chart_list = new List<LiveCharts.WinForms.CartesianChart>();
            chart_list.Add(Chart_score_round);
            // SeriesCollection series = new SeriesCollection();
            LineSeries series = new LineSeries
            {
                Values = new ChartValues<float> {0},
                StrokeThickness = 1,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 152, 0)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 0,
                PointGeometry = null
            };     
            chart_list[0].Series.Add(series);
            chart_list[0].AxisX.Add(new Axis
            {
                MinValue = 1,
                MaxValue = 360,
            });
            chart_list[0].AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = 100,
            });
            chart_list[0].AxisX.RemoveAt(0);
            chart_list[0].AxisY.RemoveAt(0);
            for (int i = 1; i <=20; i++)
            {
               
                chart_list[0].Series[0].Values.Add((float)40.5);
                Score[i] = 0;
            }
            for (int i = 20; i <= 360; i++)
            {

                chart_list[0].Series[0].Values.Add((float)-1);
                Score[i] = 0;
            }
            Dispose();
            //SerialPort.DiscardInBuffer();
        }

        private void Button_start_round(object sender, EventArgs e)
        {
            User_signalform.OpenStatus = true;
            // 宣告myIntLists 為List
            chart_list.Add(Chart_feebackscore);
            // SeriesCollection series = new SeriesCollection();
            LineSeries series = new LineSeries
            {
                Values = new ChartValues<float> { 0 },
                StrokeThickness = 1,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 152, 0)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 0,
                PointGeometry = null
            };
            chart_list[0].Series.Add(series);
            chart_list[0].AxisX.Add(new Axis
            {
                MinValue = 1,
                MaxValue = 360,
            });
            chart_list[0].AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = 100,
            });
            chart_list[0].AxisX.RemoveAt(0);
            chart_list[0].AxisY.RemoveAt(0);
            trainingstatus = true;

        }

        //一回合結束(按鈕為測試用 我寫在副函式)
        private void Button_finish_round_Click(object sender, EventArgs e)
        {
            User_signalform.Status = status;
            User_signalform.OpenStatus = false;
            float sum = 0;
            roundcount = roundcount + 1;
            status = false;
            for (int i = 1; i < SessionTime + 1; i++)
            {
                Chart_score_round.Series[0].Values.Add((float)Score[i]);
                sum = sum + Score[i];
            }
            Round[roundcount++] = sum / 360;

            // 宣告myIntLists 為List
            Roundchart_list.Add(Chart_score_round_history);
            LineSeries series = new LineSeries
            {
                Values = new ChartValues<float> { 0 },
                StrokeThickness = 1,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 152, 0)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 0,
                PointGeometry = null
            };
            Roundchart_list[0].Series.Add(series);
            Roundchart_list[0].AxisX.Add(new Axis
            {
                MinValue = 1,
                MaxValue = roundcount,
            });
            Roundchart_list[0].AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = 100,
            });
            Roundchart_list[0].AxisX.RemoveAt(0);
            Roundchart_list[0].AxisY.RemoveAt(0);

        }

        //一回合結束(按鈕為測試用 我寫在副函式)
        private void Button_Again_Click(object sender, EventArgs e)
        {
            User_signalform.Status = status;
            status = true;
            Timeserial = 0;
            count=0;
        }
    }
    }
 


