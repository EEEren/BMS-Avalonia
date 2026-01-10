using thinger.cn.BMSControl;

namespace thinger.cn.BMSPro
{
    partial class FrmUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdate));
            this.xktPlateHead4 = new xktControl.xktPlateHead(this.components);
            this.lbl_Process = new System.Windows.Forms.Label();
            this.lbl_ExcuteTime = new System.Windows.Forms.Label();
            this.pe_Process = new BMSControl.ProcessEllipse();
            this.btn_BootLoad = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.pic_Transfer = new System.Windows.Forms.PictureBox();
            this.pic_Update = new System.Windows.Forms.PictureBox();
            this.pic_Hand = new System.Windows.Forms.PictureBox();
            this.pic_Import = new System.Windows.Forms.PictureBox();
            this.pic_Reset = new System.Windows.Forms.PictureBox();
            this.label262 = new System.Windows.Forms.Label();
            this.label261 = new System.Windows.Forms.Label();
            this.label260 = new System.Windows.Forms.Label();
            this.label383 = new System.Windows.Forms.Label();
            this.label264 = new System.Windows.Forms.Label();
            this.label265 = new System.Windows.Forms.Label();
            this.label259 = new System.Windows.Forms.Label();
            this.xktPlateHead1 = new xktControl.xktPlateHead(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.xktPlateHead2 = new xktControl.xktPlateHead(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_PwdModify = new System.Windows.Forms.Button();
            this.btn_Mode3 = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Mode2 = new System.Windows.Forms.Button();
            this.btn_Restart = new System.Windows.Forms.Button();
            this.btn_Mode1 = new System.Windows.Forms.Button();
            this.xktPlateHead4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Transfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Update)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Hand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Import)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Reset)).BeginInit();
            this.xktPlateHead1.SuspendLayout();
            this.xktPlateHead2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xktPlateHead4
            // 
            this.xktPlateHead4.BorderColor = System.Drawing.Color.Gray;
            this.xktPlateHead4.Controls.Add(this.lbl_Process);
            this.xktPlateHead4.Controls.Add(this.lbl_ExcuteTime);
            this.xktPlateHead4.Controls.Add(this.pe_Process);
            this.xktPlateHead4.Controls.Add(this.btn_BootLoad);
            this.xktPlateHead4.Controls.Add(this.btn_Import);
            this.xktPlateHead4.Controls.Add(this.pic_Transfer);
            this.xktPlateHead4.Controls.Add(this.pic_Update);
            this.xktPlateHead4.Controls.Add(this.pic_Hand);
            this.xktPlateHead4.Controls.Add(this.pic_Import);
            this.xktPlateHead4.Controls.Add(this.pic_Reset);
            this.xktPlateHead4.Controls.Add(this.label262);
            this.xktPlateHead4.Controls.Add(this.label261);
            this.xktPlateHead4.Controls.Add(this.label260);
            this.xktPlateHead4.Controls.Add(this.label383);
            this.xktPlateHead4.Controls.Add(this.label264);
            this.xktPlateHead4.Controls.Add(this.label265);
            this.xktPlateHead4.Controls.Add(this.label259);
            this.xktPlateHead4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.xktPlateHead4.Location = new System.Drawing.Point(87, 21);
            this.xktPlateHead4.Name = "xktPlateHead4";
            this.xktPlateHead4.Size = new System.Drawing.Size(438, 686);
            this.xktPlateHead4.TabIndex = 18;
            this.xktPlateHead4.Text = "固件更新";
            this.xktPlateHead4.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.xktPlateHead4.ThemeForeColor = System.Drawing.Color.Black;
            // 
            // lbl_Process
            // 
            this.lbl_Process.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbl_Process.ForeColor = System.Drawing.Color.Green;
            this.lbl_Process.Location = new System.Drawing.Point(226, 605);
            this.lbl_Process.Name = "lbl_Process";
            this.lbl_Process.Size = new System.Drawing.Size(118, 25);
            this.lbl_Process.TabIndex = 29;
            this.lbl_Process.Text = "0%";
            this.lbl_Process.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ExcuteTime
            // 
            this.lbl_ExcuteTime.AutoSize = true;
            this.lbl_ExcuteTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbl_ExcuteTime.ForeColor = System.Drawing.Color.Green;
            this.lbl_ExcuteTime.Location = new System.Drawing.Point(226, 638);
            this.lbl_ExcuteTime.Name = "lbl_ExcuteTime";
            this.lbl_ExcuteTime.Size = new System.Drawing.Size(118, 25);
            this.lbl_ExcuteTime.TabIndex = 28;
            this.lbl_ExcuteTime.Text = "00：00：00";
            this.lbl_ExcuteTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pe_Process
            // 
            this.pe_Process.BackColor = System.Drawing.Color.Transparent;
            this.pe_Process.BackEllipseColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.pe_Process.CoreEllipseColor = System.Drawing.Color.Transparent;
            this.pe_Process.Font = new System.Drawing.Font("宋体", 9F);
            this.pe_Process.ForeColor = System.Drawing.Color.White;
            this.pe_Process.IsShowCoreEllipseBorder = true;
            this.pe_Process.Location = new System.Drawing.Point(141, 412);
            this.pe_Process.MaxValue = 100;
            this.pe_Process.Name = "pe_Process";
            this.pe_Process.ShowType = BMSControl.ShowType.Ring;
            this.pe_Process.Size = new System.Drawing.Size(174, 173);
            this.pe_Process.TabIndex = 27;
            this.pe_Process.Value = 0;
            this.pe_Process.ValueColor = System.Drawing.Color.LimeGreen;
            this.pe_Process.ValueMargin = 5;
            this.pe_Process.ValueType = BMSControl.ValueType.Percent;
            this.pe_Process.ValueWidth = 30;
            // 
            // btn_BootLoad
            // 
            this.btn_BootLoad.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_BootLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BootLoad.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_BootLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BootLoad.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_BootLoad.Location = new System.Drawing.Point(249, 67);
            this.btn_BootLoad.Name = "btn_BootLoad";
            this.btn_BootLoad.Size = new System.Drawing.Size(106, 40);
            this.btn_BootLoad.TabIndex = 26;
            this.btn_BootLoad.Text = "固件更新";
            this.btn_BootLoad.UseVisualStyleBackColor = false;
            this.btn_BootLoad.Click += new System.EventHandler(this.btn_BootLoad_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_Import.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Import.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Import.Location = new System.Drawing.Point(103, 67);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(106, 40);
            this.btn_Import.TabIndex = 25;
            this.btn_Import.Text = "导入配置";
            this.btn_Import.UseVisualStyleBackColor = false;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // pic_Transfer
            // 
            this.pic_Transfer.Image = global::thinger.cn.BMSPro.Properties.Resources.done;
            this.pic_Transfer.Location = new System.Drawing.Point(292, 366);
            this.pic_Transfer.Name = "pic_Transfer";
            this.pic_Transfer.Size = new System.Drawing.Size(33, 23);
            this.pic_Transfer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Transfer.TabIndex = 24;
            this.pic_Transfer.TabStop = false;
            // 
            // pic_Update
            // 
            this.pic_Update.Image = global::thinger.cn.BMSPro.Properties.Resources.done;
            this.pic_Update.Location = new System.Drawing.Point(292, 312);
            this.pic_Update.Name = "pic_Update";
            this.pic_Update.Size = new System.Drawing.Size(33, 23);
            this.pic_Update.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Update.TabIndex = 23;
            this.pic_Update.TabStop = false;
            // 
            // pic_Hand
            // 
            this.pic_Hand.Image = global::thinger.cn.BMSPro.Properties.Resources.done;
            this.pic_Hand.Location = new System.Drawing.Point(292, 258);
            this.pic_Hand.Name = "pic_Hand";
            this.pic_Hand.Size = new System.Drawing.Size(33, 23);
            this.pic_Hand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Hand.TabIndex = 22;
            this.pic_Hand.TabStop = false;
            // 
            // pic_Import
            // 
            this.pic_Import.Image = global::thinger.cn.BMSPro.Properties.Resources.done;
            this.pic_Import.Location = new System.Drawing.Point(292, 150);
            this.pic_Import.Name = "pic_Import";
            this.pic_Import.Size = new System.Drawing.Size(33, 23);
            this.pic_Import.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Import.TabIndex = 21;
            this.pic_Import.TabStop = false;
            // 
            // pic_Reset
            // 
            this.pic_Reset.Image = global::thinger.cn.BMSPro.Properties.Resources.done;
            this.pic_Reset.Location = new System.Drawing.Point(292, 204);
            this.pic_Reset.Name = "pic_Reset";
            this.pic_Reset.Size = new System.Drawing.Size(33, 23);
            this.pic_Reset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Reset.TabIndex = 20;
            this.pic_Reset.TabStop = false;
            // 
            // label262
            // 
            this.label262.Location = new System.Drawing.Point(99, 366);
            this.label262.Name = "label262";
            this.label262.Size = new System.Drawing.Size(146, 23);
            this.label262.TabIndex = 18;
            this.label262.Text = "STEP 5：跳转流程";
            this.label262.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label261
            // 
            this.label261.Location = new System.Drawing.Point(99, 312);
            this.label261.Name = "label261";
            this.label261.Size = new System.Drawing.Size(146, 23);
            this.label261.TabIndex = 17;
            this.label261.Text = "STEP 4：更新流程";
            this.label261.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label260
            // 
            this.label260.Location = new System.Drawing.Point(99, 258);
            this.label260.Name = "label260";
            this.label260.Size = new System.Drawing.Size(146, 23);
            this.label260.TabIndex = 19;
            this.label260.Text = "STEP 3：握手流程";
            this.label260.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label383
            // 
            this.label383.Location = new System.Drawing.Point(95, 640);
            this.label383.Name = "label383";
            this.label383.Size = new System.Drawing.Size(132, 23);
            this.label383.TabIndex = 14;
            this.label383.Text = "更新持续时间：";
            this.label383.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label264
            // 
            this.label264.Location = new System.Drawing.Point(94, 606);
            this.label264.Name = "label264";
            this.label264.Size = new System.Drawing.Size(132, 23);
            this.label264.TabIndex = 13;
            this.label264.Text = "更新流程进度：";
            this.label264.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label265
            // 
            this.label265.Location = new System.Drawing.Point(99, 150);
            this.label265.Name = "label265";
            this.label265.Size = new System.Drawing.Size(146, 23);
            this.label265.TabIndex = 12;
            this.label265.Text = "STEP 1：配置导入";
            this.label265.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label259
            // 
            this.label259.Location = new System.Drawing.Point(99, 204);
            this.label259.Name = "label259";
            this.label259.Size = new System.Drawing.Size(146, 23);
            this.label259.TabIndex = 11;
            this.label259.Text = "STEP 2：复位流程";
            this.label259.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xktPlateHead1
            // 
            this.xktPlateHead1.BorderColor = System.Drawing.Color.Gray;
            this.xktPlateHead1.Controls.Add(this.button4);
            this.xktPlateHead1.Controls.Add(this.button5);
            this.xktPlateHead1.Controls.Add(this.button6);
            this.xktPlateHead1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.xktPlateHead1.Location = new System.Drawing.Point(551, 21);
            this.xktPlateHead1.Name = "xktPlateHead1";
            this.xktPlateHead1.Size = new System.Drawing.Size(381, 263);
            this.xktPlateHead1.TabIndex = 19;
            this.xktPlateHead1.Text = "调试工具";
            this.xktPlateHead1.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.xktPlateHead1.ThemeForeColor = System.Drawing.Color.Black;
            // 
            // button4
            // 
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(26, 160);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(155, 53);
            this.button4.TabIndex = 8;
            this.button4.Text = "Modbus Slave";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button5.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.button5.Image = global::thinger.cn.BMSPro.Properties.Resources.battery2;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(201, 87);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(155, 53);
            this.button5.TabIndex = 9;
            this.button5.Text = "Modbus Poll";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button6.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.button6.Image = global::thinger.cn.BMSPro.Properties.Resources.battery2;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(26, 87);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(155, 53);
            this.button6.TabIndex = 10;
            this.button6.Text = "串口调试助手";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // xktPlateHead2
            // 
            this.xktPlateHead2.BorderColor = System.Drawing.Color.Gray;
            this.xktPlateHead2.Controls.Add(this.button2);
            this.xktPlateHead2.Controls.Add(this.button1);
            this.xktPlateHead2.Controls.Add(this.btn_PwdModify);
            this.xktPlateHead2.Controls.Add(this.btn_Mode3);
            this.xktPlateHead2.Controls.Add(this.btn_Reset);
            this.xktPlateHead2.Controls.Add(this.btn_Mode2);
            this.xktPlateHead2.Controls.Add(this.btn_Restart);
            this.xktPlateHead2.Controls.Add(this.btn_Mode1);
            this.xktPlateHead2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.xktPlateHead2.Location = new System.Drawing.Point(551, 313);
            this.xktPlateHead2.Name = "xktPlateHead2";
            this.xktPlateHead2.Size = new System.Drawing.Size(381, 394);
            this.xktPlateHead2.TabIndex = 20;
            this.xktPlateHead2.Text = "控制指令";
            this.xktPlateHead2.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.xktPlateHead2.ThemeForeColor = System.Drawing.Color.Black;
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.button2.Image = global::thinger.cn.BMSPro.Properties.Resources.setting;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(201, 301);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 53);
            this.button2.TabIndex = 8;
            this.button2.Text = "Mod从模式";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.button1.Image = global::thinger.cn.BMSPro.Properties.Resources.setting;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(26, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 53);
            this.button1.TabIndex = 7;
            this.button1.Text = "Mod主模式";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_PwdModify
            // 
            this.btn_PwdModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_PwdModify.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_PwdModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PwdModify.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_PwdModify.Image = global::thinger.cn.BMSPro.Properties.Resources.Restore;
            this.btn_PwdModify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PwdModify.Location = new System.Drawing.Point(201, 228);
            this.btn_PwdModify.Name = "btn_PwdModify";
            this.btn_PwdModify.Size = new System.Drawing.Size(155, 53);
            this.btn_PwdModify.TabIndex = 1;
            this.btn_PwdModify.Text = "恢复出厂设置";
            this.btn_PwdModify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_PwdModify.UseVisualStyleBackColor = true;
            // 
            // btn_Mode3
            // 
            this.btn_Mode3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Mode3.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_Mode3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Mode3.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Mode3.Image = global::thinger.cn.BMSPro.Properties.Resources.refresh;
            this.btn_Mode3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Mode3.Location = new System.Drawing.Point(26, 228);
            this.btn_Mode3.Name = "btn_Mode3";
            this.btn_Mode3.Size = new System.Drawing.Size(155, 53);
            this.btn_Mode3.TabIndex = 2;
            this.btn_Mode3.Text = "复位MCU";
            this.btn_Mode3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Mode3.UseVisualStyleBackColor = true;
            // 
            // btn_Reset
            // 
            this.btn_Reset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Reset.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Reset.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Reset.Image = ((System.Drawing.Image)(resources.GetObject("btn_Reset.Image")));
            this.btn_Reset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Reset.Location = new System.Drawing.Point(201, 155);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(155, 53);
            this.btn_Reset.TabIndex = 3;
            this.btn_Reset.Text = "关闭放电MOS";
            this.btn_Reset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Reset.UseVisualStyleBackColor = true;
            // 
            // btn_Mode2
            // 
            this.btn_Mode2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Mode2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_Mode2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Mode2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Mode2.Image = ((System.Drawing.Image)(resources.GetObject("btn_Mode2.Image")));
            this.btn_Mode2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Mode2.Location = new System.Drawing.Point(26, 155);
            this.btn_Mode2.Name = "btn_Mode2";
            this.btn_Mode2.Size = new System.Drawing.Size(155, 53);
            this.btn_Mode2.TabIndex = 4;
            this.btn_Mode2.Text = "打开放电MOS";
            this.btn_Mode2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Mode2.UseVisualStyleBackColor = true;
            // 
            // btn_Restart
            // 
            this.btn_Restart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Restart.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_Restart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Restart.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Restart.Image = global::thinger.cn.BMSPro.Properties.Resources.battery2;
            this.btn_Restart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Restart.Location = new System.Drawing.Point(201, 82);
            this.btn_Restart.Name = "btn_Restart";
            this.btn_Restart.Size = new System.Drawing.Size(155, 53);
            this.btn_Restart.TabIndex = 5;
            this.btn_Restart.Text = "关闭充电MOS";
            this.btn_Restart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Restart.UseVisualStyleBackColor = true;
            // 
            // btn_Mode1
            // 
            this.btn_Mode1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Mode1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_Mode1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Mode1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Mode1.Image = global::thinger.cn.BMSPro.Properties.Resources.battery2;
            this.btn_Mode1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Mode1.Location = new System.Drawing.Point(26, 82);
            this.btn_Mode1.Name = "btn_Mode1";
            this.btn_Mode1.Size = new System.Drawing.Size(155, 53);
            this.btn_Mode1.TabIndex = 6;
            this.btn_Mode1.Text = "打开充电MOS";
            this.btn_Mode1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Mode1.UseVisualStyleBackColor = true;
            // 
            // FrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(227)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1034, 729);
            this.Controls.Add(this.xktPlateHead2);
            this.Controls.Add(this.xktPlateHead1);
            this.Controls.Add(this.xktPlateHead4);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "固件更新";
            this.xktPlateHead4.ResumeLayout(false);
            this.xktPlateHead4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Transfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Update)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Hand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Import)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Reset)).EndInit();
            this.xktPlateHead1.ResumeLayout(false);
            this.xktPlateHead2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private xktControl.xktPlateHead xktPlateHead4;
        private xktControl.xktPlateHead xktPlateHead1;
        private System.Windows.Forms.PictureBox pic_Transfer;
        private System.Windows.Forms.PictureBox pic_Update;
        private System.Windows.Forms.PictureBox pic_Hand;
        private System.Windows.Forms.PictureBox pic_Import;
        private System.Windows.Forms.PictureBox pic_Reset;
        private System.Windows.Forms.Label label262;
        private System.Windows.Forms.Label label261;
        private System.Windows.Forms.Label label260;
        private System.Windows.Forms.Label label383;
        private System.Windows.Forms.Label label264;
        private System.Windows.Forms.Label label265;
        private System.Windows.Forms.Label label259;
        private xktControl.xktPlateHead xktPlateHead2;
        private System.Windows.Forms.Button btn_BootLoad;
        private System.Windows.Forms.Button btn_Import;
        private ProcessEllipse pe_Process;
        private System.Windows.Forms.Label lbl_Process;
        private System.Windows.Forms.Label lbl_ExcuteTime;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_PwdModify;
        private System.Windows.Forms.Button btn_Mode3;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_Mode2;
        private System.Windows.Forms.Button btn_Restart;
        private System.Windows.Forms.Button btn_Mode1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}