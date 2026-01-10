namespace thinger.cn.BMSPro
{
    partial class FrmCommSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCommSet));
            this.cmb_DevAddress = new System.Windows.Forms.ComboBox();
            this.cmb_Baud = new System.Windows.Forms.ComboBox();
            this.cmb_Port = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_SleepTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.btn_Set = new System.Windows.Forms.Button();
            this.btn_Restart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmb_DevAddress
            // 
            this.cmb_DevAddress.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.cmb_DevAddress.FormattingEnabled = true;
            this.cmb_DevAddress.Location = new System.Drawing.Point(122, 95);
            this.cmb_DevAddress.Name = "cmb_DevAddress";
            this.cmb_DevAddress.Size = new System.Drawing.Size(110, 28);
            this.cmb_DevAddress.TabIndex = 9;
            this.cmb_DevAddress.Text = "1";
            // 
            // cmb_Baud
            // 
            this.cmb_Baud.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.cmb_Baud.FormattingEnabled = true;
            this.cmb_Baud.Location = new System.Drawing.Point(351, 37);
            this.cmb_Baud.Name = "cmb_Baud";
            this.cmb_Baud.Size = new System.Drawing.Size(110, 28);
            this.cmb_Baud.TabIndex = 8;
            this.cmb_Baud.Text = "9600";
            // 
            // cmb_Port
            // 
            this.cmb_Port.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.cmb_Port.FormattingEnabled = true;
            this.cmb_Port.Location = new System.Drawing.Point(122, 37);
            this.cmb_Port.Name = "cmb_Port";
            this.cmb_Port.Size = new System.Drawing.Size(110, 28);
            this.cmb_Port.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label3.Location = new System.Drawing.Point(51, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "站地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label2.Location = new System.Drawing.Point(269, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label1.Location = new System.Drawing.Point(51, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "端口号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label4.Location = new System.Drawing.Point(269, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "延时间隔：";
            // 
            // txt_SleepTime
            // 
            this.txt_SleepTime.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txt_SleepTime.Location = new System.Drawing.Point(351, 95);
            this.txt_SleepTime.Name = "txt_SleepTime";
            this.txt_SleepTime.Size = new System.Drawing.Size(80, 26);
            this.txt_SleepTime.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label5.Location = new System.Drawing.Point(437, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "ms";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label38.ForeColor = System.Drawing.Color.Green;
            this.label38.Location = new System.Drawing.Point(33, 170);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(164, 19);
            this.label38.TabIndex = 18;
            this.label38.Text = "Tips：配置信息重启生效";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Set
            // 
            this.btn_Set.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_Set.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Set.FlatAppearance.BorderColor = System.Drawing.Color.BlanchedAlmond;
            this.btn_Set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Set.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Set.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Set.Location = new System.Drawing.Point(224, 161);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(97, 36);
            this.btn_Set.TabIndex = 20;
            this.btn_Set.Text = "保存配置";
            this.btn_Set.UseVisualStyleBackColor = false;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // btn_Restart
            // 
            this.btn_Restart.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_Restart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Restart.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Restart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Restart.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Restart.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Restart.Location = new System.Drawing.Point(353, 161);
            this.btn_Restart.Name = "btn_Restart";
            this.btn_Restart.Size = new System.Drawing.Size(97, 36);
            this.btn_Restart.TabIndex = 19;
            this.btn_Restart.Text = "重启系统";
            this.btn_Restart.UseVisualStyleBackColor = false;
            this.btn_Restart.Click += new System.EventHandler(this.btn_Restart_Click);
            // 
            // FrmCommSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 229);
            this.Controls.Add(this.btn_Set);
            this.Controls.Add(this.btn_Restart);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_SleepTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_DevAddress);
            this.Controls.Add(this.cmb_Baud);
            this.Controls.Add(this.cmb_Port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FrmCommSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通信设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_DevAddress;
        private System.Windows.Forms.ComboBox cmb_Baud;
        private System.Windows.Forms.ComboBox cmb_Port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_SleepTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.Button btn_Restart;
    }
}