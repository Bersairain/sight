﻿namespace HalconControl
{
    partial class HWindow_Final
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
                hv_MenuStrip.Dispose();

                mCtrl_HWindow.HMouseMove -= HWindowControl_HMouseMove;
            }
            if (disposing && hv_image != null)
            {
                hv_image.Dispose();
            }
            if (disposing && hv_window != null)
            {
                hv_window.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HWindow_Final));
            this.m_CtrlHStatusLabelCtrl = new System.Windows.Forms.Label();
            this.m_CtrlImageList = new System.Windows.Forms.ImageList();
            this.mCtrl_HWindow = new HalconDotNet.HWindowControl();
            this.SuspendLayout();
            // 
            // m_CtrlHStatusLabelCtrl
            // 
            this.m_CtrlHStatusLabelCtrl.AutoSize = true;
            this.m_CtrlHStatusLabelCtrl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.m_CtrlHStatusLabelCtrl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_CtrlHStatusLabelCtrl.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_CtrlHStatusLabelCtrl.ForeColor = System.Drawing.Color.Transparent;
            this.m_CtrlHStatusLabelCtrl.Location = new System.Drawing.Point(0, 452);
            this.m_CtrlHStatusLabelCtrl.Margin = new System.Windows.Forms.Padding(4);
            this.m_CtrlHStatusLabelCtrl.Name = "m_CtrlHStatusLabelCtrl";
            this.m_CtrlHStatusLabelCtrl.Size = new System.Drawing.Size(0, 23);
            this.m_CtrlHStatusLabelCtrl.TabIndex = 1;
            this.m_CtrlHStatusLabelCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_CtrlImageList
            // 
            this.m_CtrlImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_CtrlImageList.ImageStream")));
            this.m_CtrlImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.m_CtrlImageList.Images.SetKeyName(0, "TableIcon.png");
            this.m_CtrlImageList.Images.SetKeyName(1, "PicturesIcon.png");
            // 
            // mCtrl_HWindow
            // 
            this.mCtrl_HWindow.BackColor = System.Drawing.Color.Black;
            this.mCtrl_HWindow.BorderColor = System.Drawing.Color.Black;
            this.mCtrl_HWindow.Cursor = System.Windows.Forms.Cursors.Default;
            this.mCtrl_HWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mCtrl_HWindow.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.mCtrl_HWindow.Location = new System.Drawing.Point(0, 0);
            this.mCtrl_HWindow.Margin = new System.Windows.Forms.Padding(0);
            this.mCtrl_HWindow.Name = "mCtrl_HWindow";
            this.mCtrl_HWindow.Size = new System.Drawing.Size(613, 475);
            this.mCtrl_HWindow.TabIndex = 0;
            this.mCtrl_HWindow.WindowSize = new System.Drawing.Size(613, 475);
            this.mCtrl_HWindow.HMouseMove += new HalconDotNet.HMouseEventHandler(this.HWindowControl_HMouseMove);
            this.mCtrl_HWindow.MouseLeave += new System.EventHandler(this.mCtrl_HWindow_MouseLeave);
            // 
            // HWindow_Final
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.m_CtrlHStatusLabelCtrl);
            this.Controls.Add(this.mCtrl_HWindow);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HWindow_Final";
            this.Size = new System.Drawing.Size(613, 475);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_CtrlHStatusLabelCtrl;
        private System.Windows.Forms.ImageList m_CtrlImageList;
        public HalconDotNet.HWindowControl mCtrl_HWindow;
    }
}
