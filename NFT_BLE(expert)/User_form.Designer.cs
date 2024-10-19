namespace NFT_BLE_expert_
{
    partial class User_form
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.Button_Again = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.Chart_score_round = new LiveCharts.WinForms.CartesianChart();
            this.Chart_score_round_history = new LiveCharts.WinForms.CartesianChart();
            this.label_Score = new MaterialSkin.Controls.MaterialLabel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label_comport2 = new MaterialSkin.Controls.MaterialLabel();
            this.label_username2 = new MaterialSkin.Controls.MaterialLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label_enviroment = new MaterialSkin.Controls.MaterialLabel();
            this.label_headband = new MaterialSkin.Controls.MaterialLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_hint = new MaterialSkin.Controls.MaterialLabel();
            this.progress_score = new System.Windows.Forms.PictureBox();
            this.Chart_feebackscore = new LiveCharts.WinForms.CartesianChart();
            this.label_username = new MaterialSkin.Controls.MaterialLabel();
            this.Textbox_username = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.Button_finish = new MaterialSkin.Controls.MaterialRaisedButton();
            this.label_comport = new MaterialSkin.Controls.MaterialLabel();
            this.comboBox_port = new System.Windows.Forms.ComboBox();
            this.Button_Next = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Button_finish_round = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Button_comport_search = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progress_score)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Button_comport_search)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Button_Again);
            this.panel2.Controls.Add(this.materialLabel7);
            this.panel2.Controls.Add(this.Chart_score_round);
            this.panel2.Controls.Add(this.Chart_score_round_history);
            this.panel2.Controls.Add(this.label_Score);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.label_comport2);
            this.panel2.Controls.Add(this.label_username2);
            this.panel2.Location = new System.Drawing.Point(432, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 300);
            this.panel2.TabIndex = 1;
            // 
            // Button_Again
            // 
            this.Button_Again.Depth = 0;
            this.Button_Again.Location = new System.Drawing.Point(317, 235);
            this.Button_Again.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Again.Name = "Button_Again";
            this.Button_Again.Primary = true;
            this.Button_Again.Size = new System.Drawing.Size(68, 23);
            this.Button_Again.TabIndex = 5;
            this.Button_Again.Text = "Again";
            this.Button_Again.UseVisualStyleBackColor = true;
            this.Button_Again.Click += new System.EventHandler(this.Button_Again_Click);
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.Location = new System.Drawing.Point(12, 142);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(58, 19);
            this.materialLabel7.TabIndex = 11;
            this.materialLabel7.Text = "History";
            // 
            // Chart_score_round
            // 
            this.Chart_score_round.ForeColor = System.Drawing.Color.White;
            this.Chart_score_round.Location = new System.Drawing.Point(76, 41);
            this.Chart_score_round.Name = "Chart_score_round";
            this.Chart_score_round.Size = new System.Drawing.Size(283, 80);
            this.Chart_score_round.TabIndex = 9;
            this.Chart_score_round.Text = "cartesianChart3";
            // 
            // Chart_score_round_history
            // 
            this.Chart_score_round_history.Location = new System.Drawing.Point(76, 127);
            this.Chart_score_round_history.Name = "Chart_score_round_history";
            this.Chart_score_round_history.Size = new System.Drawing.Size(283, 56);
            this.Chart_score_round_history.TabIndex = 8;
            this.Chart_score_round_history.Text = "cartesianChart2";
            // 
            // label_Score
            // 
            this.label_Score.AutoSize = true;
            this.label_Score.Depth = 0;
            this.label_Score.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_Score.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_Score.Location = new System.Drawing.Point(30, 20);
            this.label_Score.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_Score.Name = "label_Score";
            this.label_Score.Size = new System.Drawing.Size(48, 19);
            this.label_Score.TabIndex = 5;
            this.label_Score.Text = "Score";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::NFT_BLE_expert_.Properties.Resources.圖片1;
            this.pictureBox3.Location = new System.Drawing.Point(34, 210);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(73, 75);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // label_comport2
            // 
            this.label_comport2.AutoSize = true;
            this.label_comport2.Depth = 0;
            this.label_comport2.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_comport2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_comport2.Location = new System.Drawing.Point(122, 255);
            this.label_comport2.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_comport2.Name = "label_comport2";
            this.label_comport2.Size = new System.Drawing.Size(62, 19);
            this.label_comport2.TabIndex = 2;
            this.label_comport2.Text = "Device :";
            // 
            // label_username2
            // 
            this.label_username2.AutoSize = true;
            this.label_username2.Depth = 0;
            this.label_username2.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_username2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_username2.Location = new System.Drawing.Point(122, 219);
            this.label_username2.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_username2.Name = "label_username2";
            this.label_username2.Size = new System.Drawing.Size(92, 19);
            this.label_username2.TabIndex = 0;
            this.label_username2.Text = "User Name :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox5);
            this.panel3.Controls.Add(this.pictureBox4);
            this.panel3.Controls.Add(this.label_enviroment);
            this.panel3.Controls.Add(this.label_headband);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.materialLabel2);
            this.panel3.Controls.Add(this.materialRaisedButton1);
            this.panel3.Controls.Add(this.materialLabel3);
            this.panel3.Location = new System.Drawing.Point(8, 390);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 300);
            this.panel3.TabIndex = 2;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::NFT_BLE_expert_.Properties.Resources.Light0;
            this.pictureBox5.Location = new System.Drawing.Point(186, 222);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(125, 25);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 7;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::NFT_BLE_expert_.Properties.Resources.Light0;
            this.pictureBox4.Location = new System.Drawing.Point(186, 179);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(125, 25);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // label_enviroment
            // 
            this.label_enviroment.AutoSize = true;
            this.label_enviroment.Depth = 0;
            this.label_enviroment.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_enviroment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_enviroment.Location = new System.Drawing.Point(61, 225);
            this.label_enviroment.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_enviroment.Name = "label_enviroment";
            this.label_enviroment.Size = new System.Drawing.Size(93, 19);
            this.label_enviroment.TabIndex = 5;
            this.label_enviroment.Text = "Environment";
            // 
            // label_headband
            // 
            this.label_headband.AutoSize = true;
            this.label_headband.Depth = 0;
            this.label_headband.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_headband.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_headband.Location = new System.Drawing.Point(68, 185);
            this.label_headband.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_headband.Name = "label_headband";
            this.label_headband.Size = new System.Drawing.Size(76, 19);
            this.label_headband.TabIndex = 4;
            this.label_headband.Text = "Headband";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::NFT_BLE_expert_.Properties.Resources.CHECK;
            this.pictureBox2.Location = new System.Drawing.Point(92, 49);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(205, 114);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(49, 185);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(0, 19);
            this.materialLabel2.TabIndex = 3;
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Location = new System.Drawing.Point(317, 264);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(68, 23);
            this.materialRaisedButton1.TabIndex = 4;
            this.materialRaisedButton1.Text = "START";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.Button_start_round);
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(106, 20);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(192, 19);
            this.materialLabel3.TabIndex = 0;
            this.materialLabel3.Text = "Headband and Environment";
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(30, 36);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(92, 19);
            this.materialLabel5.TabIndex = 0;
            this.materialLabel5.Text = "User Name :";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(276, 36);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(48, 19);
            this.materialLabel1.TabIndex = 5;
            this.materialLabel1.Text = "Score";
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(30, 260);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(76, 19);
            this.materialLabel6.TabIndex = 8;
            this.materialLabel6.Text = "Headband";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(145, 258);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(93, 19);
            this.materialLabel4.TabIndex = 9;
            this.materialLabel4.Text = "Environment";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::NFT_BLE_expert_.Properties.Resources.Light0;
            this.pictureBox7.Location = new System.Drawing.Point(112, 254);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(18, 25);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 10;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::NFT_BLE_expert_.Properties.Resources.Light0;
            this.pictureBox6.Location = new System.Drawing.Point(245, 255);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(21, 25);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 11;
            this.pictureBox6.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label_hint);
            this.panel4.Controls.Add(this.progress_score);
            this.panel4.Controls.Add(this.pictureBox6);
            this.panel4.Controls.Add(this.pictureBox7);
            this.panel4.Controls.Add(this.materialLabel4);
            this.panel4.Controls.Add(this.materialLabel6);
            this.panel4.Controls.Add(this.Chart_feebackscore);
            this.panel4.Controls.Add(this.materialLabel1);
            this.panel4.Controls.Add(this.materialLabel5);
            this.panel4.Location = new System.Drawing.Point(432, 390);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(400, 300);
            this.panel4.TabIndex = 3;
            // 
            // label_hint
            // 
            this.label_hint.AutoSize = true;
            this.label_hint.Depth = 0;
            this.label_hint.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_hint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_hint.Location = new System.Drawing.Point(177, 112);
            this.label_hint.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_hint.Name = "label_hint";
            this.label_hint.Size = new System.Drawing.Size(37, 19);
            this.label_hint.TabIndex = 13;
            this.label_hint.Text = "Hint";
            // 
            // progress_score
            // 
            this.progress_score.Location = new System.Drawing.Point(44, 92);
            this.progress_score.Name = "progress_score";
            this.progress_score.Size = new System.Drawing.Size(325, 62);
            this.progress_score.TabIndex = 12;
            this.progress_score.TabStop = false;
            // 
            // Chart_feebackscore
            // 
            this.Chart_feebackscore.Location = new System.Drawing.Point(34, 160);
            this.Chart_feebackscore.Name = "Chart_feebackscore";
            this.Chart_feebackscore.Size = new System.Drawing.Size(325, 65);
            this.Chart_feebackscore.TabIndex = 7;
            this.Chart_feebackscore.Text = "cartesianChart1";
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Depth = 0;
            this.label_username.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_username.Location = new System.Drawing.Point(143, 54);
            this.label_username.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(92, 19);
            this.label_username.TabIndex = 0;
            this.label_username.Text = "User Name :";
            // 
            // Textbox_username
            // 
            this.Textbox_username.Depth = 0;
            this.Textbox_username.Hint = "";
            this.Textbox_username.Location = new System.Drawing.Point(242, 52);
            this.Textbox_username.MouseState = MaterialSkin.MouseState.HOVER;
            this.Textbox_username.Name = "Textbox_username";
            this.Textbox_username.PasswordChar = '\0';
            this.Textbox_username.SelectedText = "";
            this.Textbox_username.SelectionLength = 0;
            this.Textbox_username.SelectionStart = 0;
            this.Textbox_username.Size = new System.Drawing.Size(143, 23);
            this.Textbox_username.TabIndex = 1;
            this.Textbox_username.UseSystemPasswordChar = false;
            // 
            // Button_finish
            // 
            this.Button_finish.Depth = 0;
            this.Button_finish.Location = new System.Drawing.Point(291, 202);
            this.Button_finish.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_finish.Name = "Button_finish";
            this.Button_finish.Primary = true;
            this.Button_finish.Size = new System.Drawing.Size(68, 23);
            this.Button_finish.TabIndex = 4;
            this.Button_finish.Text = "Finish";
            this.Button_finish.UseVisualStyleBackColor = true;
            this.Button_finish.Click += new System.EventHandler(this.Button_finish_Click);
            // 
            // label_comport
            // 
            this.label_comport.AutoSize = true;
            this.label_comport.Depth = 0;
            this.label_comport.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_comport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_comport.Location = new System.Drawing.Point(65, 151);
            this.label_comport.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_comport.Name = "label_comport";
            this.label_comport.Size = new System.Drawing.Size(62, 19);
            this.label_comport.TabIndex = 2;
            this.label_comport.Text = "Device :";
            // 
            // comboBox_port
            // 
            this.comboBox_port.FormattingEnabled = true;
            this.comboBox_port.Location = new System.Drawing.Point(149, 151);
            this.comboBox_port.Name = "comboBox_port";
            this.comboBox_port.Size = new System.Drawing.Size(121, 20);
            this.comboBox_port.TabIndex = 3;
            // 
            // Button_Next
            // 
            this.Button_Next.Depth = 0;
            this.Button_Next.Location = new System.Drawing.Point(27, 198);
            this.Button_Next.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_Next.Name = "Button_Next";
            this.Button_Next.Primary = true;
            this.Button_Next.Size = new System.Drawing.Size(75, 23);
            this.Button_Next.TabIndex = 4;
            this.Button_Next.Text = "Login";
            this.Button_Next.UseVisualStyleBackColor = true;
            this.Button_Next.Click += new System.EventHandler(this.Button_login_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NFT_BLE_expert_.Properties.Resources.圖片1;
            this.pictureBox1.Location = new System.Drawing.Point(27, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Button_finish_round
            // 
            this.Button_finish_round.Depth = 0;
            this.Button_finish_round.Location = new System.Drawing.Point(167, 255);
            this.Button_finish_round.MouseState = MaterialSkin.MouseState.HOVER;
            this.Button_finish_round.Name = "Button_finish_round";
            this.Button_finish_round.Primary = true;
            this.Button_finish_round.Size = new System.Drawing.Size(68, 23);
            this.Button_finish_round.TabIndex = 5;
            this.Button_finish_round.Text = "Next";
            this.Button_finish_round.UseVisualStyleBackColor = true;
            this.Button_finish_round.Click += new System.EventHandler(this.Button_finish_round_Click);
            // 
            // Button_comport_search
            // 
            this.Button_comport_search.Image = global::NFT_BLE_expert_.Properties.Resources.comfresh;
            this.Button_comport_search.Location = new System.Drawing.Point(276, 151);
            this.Button_comport_search.Name = "Button_comport_search";
            this.Button_comport_search.Size = new System.Drawing.Size(26, 20);
            this.Button_comport_search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Button_comport_search.TabIndex = 5;
            this.Button_comport_search.TabStop = false;
            this.Button_comport_search.Click += new System.EventHandler(this.Button_comport_search_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Button_comport_search);
            this.panel1.Controls.Add(this.Button_finish_round);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.Button_Next);
            this.panel1.Controls.Add(this.comboBox_port);
            this.panel1.Controls.Add(this.label_comport);
            this.panel1.Controls.Add(this.Button_finish);
            this.panel1.Controls.Add(this.Textbox_username);
            this.panel1.Controls.Add(this.label_username);
            this.panel1.Location = new System.Drawing.Point(8, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 300);
            this.panel1.TabIndex = 0;
            // 
            // User_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "User_form";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progress_score)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Button_comport_search)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private MaterialSkin.Controls.MaterialLabel label_comport2;
        private MaterialSkin.Controls.MaterialLabel label_username2;
        private MaterialSkin.Controls.MaterialLabel label_Score;
        private System.Windows.Forms.Panel panel3;
        private MaterialSkin.Controls.MaterialLabel label_enviroment;
        private MaterialSkin.Controls.MaterialLabel label_headband;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private LiveCharts.WinForms.CartesianChart Chart_score_round;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private LiveCharts.WinForms.CartesianChart Chart_score_round_history;
        private MaterialSkin.Controls.MaterialRaisedButton Button_Again;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox progress_score;
        private LiveCharts.WinForms.CartesianChart Chart_feebackscore;
        private MaterialSkin.Controls.MaterialLabel label_hint;
        private MaterialSkin.Controls.MaterialLabel label_username;
        private MaterialSkin.Controls.MaterialSingleLineTextField Textbox_username;
        private MaterialSkin.Controls.MaterialRaisedButton Button_finish;
        private MaterialSkin.Controls.MaterialLabel label_comport;
        private System.Windows.Forms.ComboBox comboBox_port;
        private MaterialSkin.Controls.MaterialRaisedButton Button_Next;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialRaisedButton Button_finish_round;
        private System.Windows.Forms.PictureBox Button_comport_search;
        private System.Windows.Forms.Panel panel1;
    }
}

