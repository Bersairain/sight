namespace Sight
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.materialTabControl1 = new ReaLTaiizor.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.hWindow_Final1_grab = new HalconControl.HWindow_Final();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gain = new System.Windows.Forms.TextBox();
            this.writein = new System.Windows.Forms.Button();
            this.readin = new System.Windows.Forms.Button();
            this.shutter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.savejpg = new System.Windows.Forms.Button();
            this.savebmp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_load = new System.Windows.Forms.Button();
            this.Continuegarb = new System.Windows.Forms.Button();
            this.Stopgarb = new System.Windows.Forms.Button();
            this.Signalgarb = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.camclose = new System.Windows.Forms.Button();
            this.camopen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Searchcamera = new ReaLTaiizor.Controls.MaterialButton();
            this.cbcamera = new System.Windows.Forms.ComboBox();
            this.tcommun = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_sockettcpoff = new System.Windows.Forms.Button();
            this.btn_sendsockettcp = new System.Windows.Forms.Button();
            this.RTX_socketsend = new System.Windows.Forms.RichTextBox();
            this.RTX_socketreceive = new System.Windows.Forms.RichTextBox();
            this.btn_sockettcpon = new System.Windows.Forms.Button();
            this.txt_portsev = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_ipsev = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txt_modbustcpwrite = new System.Windows.Forms.RichTextBox();
            this.btn_modbustcpread = new System.Windows.Forms.Button();
            this.btn_modbustcpwrite = new System.Windows.Forms.Button();
            this.txt_modbustcpread = new System.Windows.Forms.RichTextBox();
            this.btn_modbustcpoff = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_modbustcpon = new System.Windows.Forms.Button();
            this.txt_number = new System.Windows.Forms.TextBox();
            this.txt_modbusip = new System.Windows.Forms.TextBox();
            this.txt_startaddress = new System.Windows.Forms.TextBox();
            this.txt_slaveid = new System.Windows.Forms.TextBox();
            this.txt_modbusport = new System.Windows.Forms.TextBox();
            this.communcombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.materialTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tcommun.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Controls.Add(this.tcommun);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.ImageList = this.imageList1;
            this.materialTabControl1.ItemSize = new System.Drawing.Size(72, 27);
            this.materialTabControl1.Location = new System.Drawing.Point(0, 64);
            this.materialTabControl1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1277, 893);
            this.materialTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.ImageKey = "菜单.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1269, 858);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "菜单";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.hWindow_Final1_grab);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.Searchcamera);
            this.tabPage2.Controls.Add(this.cbcamera);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.ImageKey = "相机.png";
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1269, 858);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "相机";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // hWindow_Final1_grab
            // 
            this.hWindow_Final1_grab.BackColor = System.Drawing.Color.Transparent;
            this.hWindow_Final1_grab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hWindow_Final1_grab.DrawModel = false;
            this.hWindow_Final1_grab.Image = null;
            this.hWindow_Final1_grab.Location = new System.Drawing.Point(15, 137);
            this.hWindow_Final1_grab.Margin = new System.Windows.Forms.Padding(6);
            this.hWindow_Final1_grab.Name = "hWindow_Final1_grab";
            this.hWindow_Final1_grab.Size = new System.Drawing.Size(736, 663);
            this.hWindow_Final1_grab.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gain);
            this.groupBox3.Controls.Add(this.writein);
            this.groupBox3.Controls.Add(this.readin);
            this.groupBox3.Controls.Add(this.shutter);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(760, 569);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(441, 231);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数";
            // 
            // gain
            // 
            this.gain.Location = new System.Drawing.Point(157, 118);
            this.gain.Name = "gain";
            this.gain.Size = new System.Drawing.Size(205, 34);
            this.gain.TabIndex = 5;
            // 
            // writein
            // 
            this.writein.Location = new System.Drawing.Point(220, 167);
            this.writein.Name = "writein";
            this.writein.Size = new System.Drawing.Size(142, 48);
            this.writein.TabIndex = 0;
            this.writein.Text = "写入";
            this.writein.UseVisualStyleBackColor = true;
            this.writein.Click += new System.EventHandler(this.writein_Click);
            // 
            // readin
            // 
            this.readin.Location = new System.Drawing.Point(58, 167);
            this.readin.Name = "readin";
            this.readin.Size = new System.Drawing.Size(142, 48);
            this.readin.TabIndex = 0;
            this.readin.Text = "读取";
            this.readin.UseVisualStyleBackColor = true;
            this.readin.Click += new System.EventHandler(this.readin_Click);
            // 
            // shutter
            // 
            this.shutter.Location = new System.Drawing.Point(157, 43);
            this.shutter.Name = "shutter";
            this.shutter.Size = new System.Drawing.Size(205, 34);
            this.shutter.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "增  益";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "曝  光";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.savejpg);
            this.groupBox4.Controls.Add(this.savebmp);
            this.groupBox4.Location = new System.Drawing.Point(760, 430);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(441, 124);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "存图";
            // 
            // savejpg
            // 
            this.savejpg.Location = new System.Drawing.Point(250, 45);
            this.savejpg.Name = "savejpg";
            this.savejpg.Size = new System.Drawing.Size(142, 48);
            this.savejpg.TabIndex = 0;
            this.savejpg.Text = "保存jpg";
            this.savejpg.UseVisualStyleBackColor = true;
            this.savejpg.Click += new System.EventHandler(this.savejpg_Click);
            // 
            // savebmp
            // 
            this.savebmp.Location = new System.Drawing.Point(58, 45);
            this.savebmp.Name = "savebmp";
            this.savebmp.Size = new System.Drawing.Size(142, 48);
            this.savebmp.TabIndex = 0;
            this.savebmp.Text = "保存bmp";
            this.savebmp.UseVisualStyleBackColor = true;
            this.savebmp.Click += new System.EventHandler(this.savebmp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_load);
            this.groupBox2.Controls.Add(this.Continuegarb);
            this.groupBox2.Controls.Add(this.Stopgarb);
            this.groupBox2.Controls.Add(this.Signalgarb);
            this.groupBox2.Location = new System.Drawing.Point(760, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 215);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "采集";
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(250, 142);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(142, 48);
            this.btn_load.TabIndex = 1;
            this.btn_load.Text = "加载图像";
            this.btn_load.UseVisualStyleBackColor = true;
            // 
            // Continuegarb
            // 
            this.Continuegarb.Location = new System.Drawing.Point(250, 59);
            this.Continuegarb.Name = "Continuegarb";
            this.Continuegarb.Size = new System.Drawing.Size(142, 48);
            this.Continuegarb.TabIndex = 0;
            this.Continuegarb.Text = "连续采集";
            this.Continuegarb.UseVisualStyleBackColor = true;
            this.Continuegarb.Click += new System.EventHandler(this.Continuegarb_Click);
            // 
            // Stopgarb
            // 
            this.Stopgarb.Location = new System.Drawing.Point(58, 142);
            this.Stopgarb.Name = "Stopgarb";
            this.Stopgarb.Size = new System.Drawing.Size(142, 48);
            this.Stopgarb.TabIndex = 0;
            this.Stopgarb.Text = "停止采集";
            this.Stopgarb.UseVisualStyleBackColor = true;
            this.Stopgarb.Click += new System.EventHandler(this.Stopgarb_Click);
            // 
            // Signalgarb
            // 
            this.Signalgarb.Location = new System.Drawing.Point(58, 59);
            this.Signalgarb.Name = "Signalgarb";
            this.Signalgarb.Size = new System.Drawing.Size(142, 48);
            this.Signalgarb.TabIndex = 0;
            this.Signalgarb.Text = "单次采集";
            this.Signalgarb.UseVisualStyleBackColor = true;
            this.Signalgarb.Click += new System.EventHandler(this.Signalgarb_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.camclose);
            this.groupBox1.Controls.Add(this.camopen);
            this.groupBox1.Location = new System.Drawing.Point(760, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 166);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "初始化";
            // 
            // camclose
            // 
            this.camclose.Location = new System.Drawing.Point(250, 59);
            this.camclose.Name = "camclose";
            this.camclose.Size = new System.Drawing.Size(142, 48);
            this.camclose.TabIndex = 0;
            this.camclose.Text = "断开相机";
            this.camclose.UseVisualStyleBackColor = true;
            this.camclose.Click += new System.EventHandler(this.camclose_Click);
            // 
            // camopen
            // 
            this.camopen.Location = new System.Drawing.Point(58, 59);
            this.camopen.Name = "camopen";
            this.camopen.Size = new System.Drawing.Size(142, 48);
            this.camopen.TabIndex = 0;
            this.camopen.Text = "连接相机";
            this.camopen.UseVisualStyleBackColor = true;
            this.camopen.Click += new System.EventHandler(this.camopen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "实时图像";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "相机列表";
            // 
            // Searchcamera
            // 
            this.Searchcamera.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Searchcamera.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Searchcamera.Depth = 0;
            this.Searchcamera.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Searchcamera.HighEmphasis = true;
            this.Searchcamera.Icon = null;
            this.Searchcamera.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.Searchcamera.Location = new System.Drawing.Point(584, 33);
            this.Searchcamera.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Searchcamera.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.Searchcamera.Name = "Searchcamera";
            this.Searchcamera.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Searchcamera.Size = new System.Drawing.Size(85, 36);
            this.Searchcamera.TabIndex = 1;
            this.Searchcamera.Text = "查找相机";
            this.Searchcamera.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Searchcamera.UseAccentColor = false;
            this.Searchcamera.UseVisualStyleBackColor = true;
            this.Searchcamera.Click += new System.EventHandler(this.Searchcamera_Click);
            // 
            // cbcamera
            // 
            this.cbcamera.FormattingEnabled = true;
            this.cbcamera.Location = new System.Drawing.Point(133, 33);
            this.cbcamera.Name = "cbcamera";
            this.cbcamera.Size = new System.Drawing.Size(429, 31);
            this.cbcamera.TabIndex = 0;
            // 
            // tcommun
            // 
            this.tcommun.Controls.Add(this.groupBox6);
            this.tcommun.Controls.Add(this.groupBox5);
            this.tcommun.Controls.Add(this.communcombo);
            this.tcommun.Controls.Add(this.label5);
            this.tcommun.ImageKey = "asset-monitor_line.png";
            this.tcommun.Location = new System.Drawing.Point(4, 31);
            this.tcommun.Name = "tcommun";
            this.tcommun.Padding = new System.Windows.Forms.Padding(3);
            this.tcommun.Size = new System.Drawing.Size(1269, 858);
            this.tcommun.TabIndex = 2;
            this.tcommun.Text = "通讯";
            this.tcommun.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_sockettcpoff);
            this.groupBox6.Controls.Add(this.btn_sendsockettcp);
            this.groupBox6.Controls.Add(this.RTX_socketsend);
            this.groupBox6.Controls.Add(this.RTX_socketreceive);
            this.groupBox6.Controls.Add(this.btn_sockettcpon);
            this.groupBox6.Controls.Add(this.txt_portsev);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.txt_ipsev);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(443, 79);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(388, 519);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "TCP";
            // 
            // btn_sockettcpoff
            // 
            this.btn_sockettcpoff.BackColor = System.Drawing.Color.Transparent;
            this.btn_sockettcpoff.Location = new System.Drawing.Point(277, 140);
            this.btn_sockettcpoff.Name = "btn_sockettcpoff";
            this.btn_sockettcpoff.Size = new System.Drawing.Size(93, 40);
            this.btn_sockettcpoff.TabIndex = 8;
            this.btn_sockettcpoff.Text = "断开";
            this.btn_sockettcpoff.UseVisualStyleBackColor = false;
            this.btn_sockettcpoff.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // btn_sendsockettcp
            // 
            this.btn_sendsockettcp.Location = new System.Drawing.Point(6, 473);
            this.btn_sendsockettcp.Name = "btn_sendsockettcp";
            this.btn_sendsockettcp.Size = new System.Drawing.Size(93, 40);
            this.btn_sendsockettcp.TabIndex = 7;
            this.btn_sendsockettcp.Text = "发送";
            this.btn_sendsockettcp.UseVisualStyleBackColor = true;
            this.btn_sendsockettcp.Click += new System.EventHandler(this.bt_sendcus_Click);
            // 
            // RTX_socketsend
            // 
            this.RTX_socketsend.Location = new System.Drawing.Point(6, 361);
            this.RTX_socketsend.Name = "RTX_socketsend";
            this.RTX_socketsend.Size = new System.Drawing.Size(376, 107);
            this.RTX_socketsend.TabIndex = 6;
            this.RTX_socketsend.Text = "";
            // 
            // RTX_socketreceive
            // 
            this.RTX_socketreceive.Location = new System.Drawing.Point(6, 222);
            this.RTX_socketreceive.Name = "RTX_socketreceive";
            this.RTX_socketreceive.Size = new System.Drawing.Size(376, 107);
            this.RTX_socketreceive.TabIndex = 6;
            this.RTX_socketreceive.Text = "";
            // 
            // btn_sockettcpon
            // 
            this.btn_sockettcpon.Location = new System.Drawing.Point(19, 140);
            this.btn_sockettcpon.Name = "btn_sockettcpon";
            this.btn_sockettcpon.Size = new System.Drawing.Size(93, 40);
            this.btn_sockettcpon.TabIndex = 5;
            this.btn_sockettcpon.Text = "确认";
            this.btn_sockettcpon.UseVisualStyleBackColor = true;
            this.btn_sockettcpon.Click += new System.EventHandler(this.bt_comfirmsev_Click);
            // 
            // txt_portsev
            // 
            this.txt_portsev.Location = new System.Drawing.Point(123, 85);
            this.txt_portsev.Name = "txt_portsev";
            this.txt_portsev.Size = new System.Drawing.Size(227, 34);
            this.txt_portsev.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 335);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 23);
            this.label11.TabIndex = 3;
            this.label11.Text = "发送";
            // 
            // txt_ipsev
            // 
            this.txt_ipsev.Location = new System.Drawing.Point(123, 27);
            this.txt_ipsev.Name = "txt_ipsev";
            this.txt_ipsev.Size = new System.Drawing.Size(227, 34);
            this.txt_ipsev.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 196);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 23);
            this.label10.TabIndex = 3;
            this.label10.Text = "接收";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 23);
            this.label8.TabIndex = 3;
            this.label8.Text = "端口号";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 23);
            this.label7.TabIndex = 3;
            this.label7.Text = "目标地址";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txt_modbustcpwrite);
            this.groupBox5.Controls.Add(this.btn_modbustcpread);
            this.groupBox5.Controls.Add(this.btn_modbustcpwrite);
            this.groupBox5.Controls.Add(this.txt_modbustcpread);
            this.groupBox5.Controls.Add(this.btn_modbustcpoff);
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.btn_modbustcpon);
            this.groupBox5.Controls.Add(this.txt_number);
            this.groupBox5.Controls.Add(this.txt_modbusip);
            this.groupBox5.Controls.Add(this.txt_startaddress);
            this.groupBox5.Controls.Add(this.txt_slaveid);
            this.groupBox5.Controls.Add(this.txt_modbusport);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(6, 79);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(390, 583);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ModbusTcp通讯";
            // 
            // txt_modbustcpwrite
            // 
            this.txt_modbustcpwrite.Location = new System.Drawing.Point(6, 497);
            this.txt_modbustcpwrite.Name = "txt_modbustcpwrite";
            this.txt_modbustcpwrite.Size = new System.Drawing.Size(378, 34);
            this.txt_modbustcpwrite.TabIndex = 9;
            this.txt_modbustcpwrite.Text = "";
            // 
            // btn_modbustcpread
            // 
            this.btn_modbustcpread.Location = new System.Drawing.Point(6, 428);
            this.btn_modbustcpread.Name = "btn_modbustcpread";
            this.btn_modbustcpread.Size = new System.Drawing.Size(93, 40);
            this.btn_modbustcpread.TabIndex = 7;
            this.btn_modbustcpread.Text = "读取";
            this.btn_modbustcpread.UseVisualStyleBackColor = true;
            this.btn_modbustcpread.Click += new System.EventHandler(this.btn_modbustcpread_Click);
            // 
            // btn_modbustcpwrite
            // 
            this.btn_modbustcpwrite.Location = new System.Drawing.Point(6, 537);
            this.btn_modbustcpwrite.Name = "btn_modbustcpwrite";
            this.btn_modbustcpwrite.Size = new System.Drawing.Size(93, 40);
            this.btn_modbustcpwrite.TabIndex = 7;
            this.btn_modbustcpwrite.Text = "写入";
            this.btn_modbustcpwrite.UseVisualStyleBackColor = true;
            this.btn_modbustcpwrite.Click += new System.EventHandler(this.btn_modbustcpsend_Click);
            // 
            // txt_modbustcpread
            // 
            this.txt_modbustcpread.Location = new System.Drawing.Point(6, 390);
            this.txt_modbustcpread.Name = "txt_modbustcpread";
            this.txt_modbustcpread.Size = new System.Drawing.Size(378, 34);
            this.txt_modbustcpread.TabIndex = 9;
            this.txt_modbustcpread.Text = "";
            // 
            // btn_modbustcpoff
            // 
            this.btn_modbustcpoff.BackColor = System.Drawing.Color.Transparent;
            this.btn_modbustcpoff.Location = new System.Drawing.Point(275, 140);
            this.btn_modbustcpoff.Name = "btn_modbustcpoff";
            this.btn_modbustcpoff.Size = new System.Drawing.Size(93, 40);
            this.btn_modbustcpoff.TabIndex = 8;
            this.btn_modbustcpoff.Text = "断开";
            this.btn_modbustcpoff.UseVisualStyleBackColor = false;
            this.btn_modbustcpoff.Click += new System.EventHandler(this.btn_modbustcpoff_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "读取线圈",
            "读取离散输入",
            "读取保持寄存器",
            "读取输入寄存器",
            "写单个线圈",
            "写多个线圈",
            "写单个寄存器",
            "写多个寄存器"});
            this.comboBox1.Location = new System.Drawing.Point(141, 222);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(227, 31);
            this.comboBox1.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 23);
            this.label6.TabIndex = 3;
            this.label6.Text = "目标地址";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 335);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(102, 23);
            this.label15.TabIndex = 3;
            this.label15.Text = "读写位数";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(174, 284);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 23);
            this.label14.TabIndex = 3;
            this.label14.Text = "起始地址";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 471);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 23);
            this.label17.TabIndex = 3;
            this.label17.Text = "写入";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 284);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 23);
            this.label13.TabIndex = 3;
            this.label13.Text = "从站编号";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 225);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 23);
            this.label12.TabIndex = 3;
            this.label12.Text = "功能选择";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 364);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 23);
            this.label16.TabIndex = 3;
            this.label16.Text = "读取";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 23);
            this.label9.TabIndex = 3;
            this.label9.Text = "端口号";
            // 
            // btn_modbustcpon
            // 
            this.btn_modbustcpon.Location = new System.Drawing.Point(27, 140);
            this.btn_modbustcpon.Name = "btn_modbustcpon";
            this.btn_modbustcpon.Size = new System.Drawing.Size(93, 40);
            this.btn_modbustcpon.TabIndex = 5;
            this.btn_modbustcpon.Text = "确认";
            this.btn_modbustcpon.UseVisualStyleBackColor = true;
            this.btn_modbustcpon.Click += new System.EventHandler(this.btn_modbustcpon_Click);
            // 
            // txt_number
            // 
            this.txt_number.Location = new System.Drawing.Point(120, 332);
            this.txt_number.Name = "txt_number";
            this.txt_number.Size = new System.Drawing.Size(51, 34);
            this.txt_number.TabIndex = 4;
            // 
            // txt_modbusip
            // 
            this.txt_modbusip.Location = new System.Drawing.Point(141, 25);
            this.txt_modbusip.Name = "txt_modbusip";
            this.txt_modbusip.Size = new System.Drawing.Size(227, 34);
            this.txt_modbusip.TabIndex = 4;
            // 
            // txt_startaddress
            // 
            this.txt_startaddress.Location = new System.Drawing.Point(289, 281);
            this.txt_startaddress.Name = "txt_startaddress";
            this.txt_startaddress.Size = new System.Drawing.Size(51, 34);
            this.txt_startaddress.TabIndex = 4;
            // 
            // txt_slaveid
            // 
            this.txt_slaveid.Location = new System.Drawing.Point(120, 281);
            this.txt_slaveid.Name = "txt_slaveid";
            this.txt_slaveid.Size = new System.Drawing.Size(51, 34);
            this.txt_slaveid.TabIndex = 4;
            // 
            // txt_modbusport
            // 
            this.txt_modbusport.Location = new System.Drawing.Point(141, 78);
            this.txt_modbusport.Name = "txt_modbusport";
            this.txt_modbusport.Size = new System.Drawing.Size(227, 34);
            this.txt_modbusport.TabIndex = 4;
            // 
            // communcombo
            // 
            this.communcombo.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.communcombo.FormattingEnabled = true;
            this.communcombo.Items.AddRange(new object[] {
            "ModbusTcp从站",
            "ModbusTcp主站",
            "TCP客户端",
            "TCP服务器"});
            this.communcombo.Location = new System.Drawing.Point(181, 23);
            this.communcombo.Name = "communcombo";
            this.communcombo.Size = new System.Drawing.Size(261, 31);
            this.communcombo.TabIndex = 1;
            this.communcombo.SelectedIndexChanged += new System.EventHandler(this.communcombo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 37);
            this.label5.TabIndex = 0;
            this.label5.Text = "通讯模式";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "相机.png");
            this.imageList1.Images.SetKeyName(1, "菜单.png");
            this.imageList1.Images.SetKeyName(2, "asset-monitor_line.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 960);
            this.Controls.Add(this.materialTabControl1);
            this.DrawerShowIconsWhenHidden = true;
            this.DrawerTabControl = this.materialTabControl1;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 64, 3, 3);
            this.Text = "相机控制";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tcommun.ResumeLayout(false);
            this.tcommun.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cbcamera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button camclose;
        private System.Windows.Forms.Button camopen;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Continuegarb;
        private System.Windows.Forms.Button Stopgarb;
        private System.Windows.Forms.Button Signalgarb;
        private System.Windows.Forms.Label label2;
        private ReaLTaiizor.Controls.MaterialButton Searchcamera;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button savejpg;
        private System.Windows.Forms.Button readin;
        private System.Windows.Forms.TextBox gain;
        private System.Windows.Forms.TextBox shutter;
        private System.Windows.Forms.Button writein;
        private System.Windows.Forms.Button savebmp;
        private HalconControl.HWindow_Final hWindow_Final1_grab;
        private System.Windows.Forms.TabPage tcommun;
        private System.Windows.Forms.ComboBox communcombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txt_portsev;
        private System.Windows.Forms.TextBox txt_ipsev;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_sendsockettcp;
        private System.Windows.Forms.RichTextBox RTX_socketreceive;
        private System.Windows.Forms.Button btn_sockettcpon;
        private System.Windows.Forms.RichTextBox RTX_socketsend;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_sockettcpoff;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_modbusip;
        private System.Windows.Forms.TextBox txt_modbusport;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btn_modbustcpoff;
        private System.Windows.Forms.Button btn_modbustcpon;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_startaddress;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_number;
        private System.Windows.Forms.TextBox txt_slaveid;
        private System.Windows.Forms.RichTextBox txt_modbustcpwrite;
        private System.Windows.Forms.Button btn_modbustcpwrite;
        private System.Windows.Forms.RichTextBox txt_modbustcpread;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btn_modbustcpread;
    }
}

