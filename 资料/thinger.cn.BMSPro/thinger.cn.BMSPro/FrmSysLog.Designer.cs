namespace thinger.cn.BMSPro
{
    partial class FrmSysLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSysLog));
            this.btn_Query = new System.Windows.Forms.Button();
            this.Panel_Param1 = new xktControl.xktPlateHead(this.components);
            this.btn_Recently100 = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_Recently50 = new System.Windows.Forms.Button();
            this.cmb_LogType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dpt_End = new System.Windows.Forms.DateTimePicker();
            this.dpt_Start = new System.Windows.Forms.DateTimePicker();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.dgv_Log = new System.Windows.Forms.DataGridView();
            this.LogTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.Panel_Param1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Log)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Query
            // 
            this.btn_Query.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_Query.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Query.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Query.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Query.Location = new System.Drawing.Point(408, 109);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(102, 36);
            this.btn_Query.TabIndex = 0;
            this.btn_Query.Text = "时间查询";
            this.btn_Query.UseVisualStyleBackColor = false;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // Panel_Param1
            // 
            this.Panel_Param1.BorderColor = System.Drawing.Color.Gray;
            this.Panel_Param1.Controls.Add(this.btn_Recently100);
            this.Panel_Param1.Controls.Add(this.btn_Export);
            this.Panel_Param1.Controls.Add(this.btn_Recently50);
            this.Panel_Param1.Controls.Add(this.cmb_LogType);
            this.Panel_Param1.Controls.Add(this.label1);
            this.Panel_Param1.Controls.Add(this.dpt_End);
            this.Panel_Param1.Controls.Add(this.dpt_Start);
            this.Panel_Param1.Controls.Add(this.label46);
            this.Panel_Param1.Controls.Add(this.label47);
            this.Panel_Param1.Controls.Add(this.dgv_Log);
            this.Panel_Param1.Controls.Add(this.btn_Print);
            this.Panel_Param1.Controls.Add(this.btn_Clear);
            this.Panel_Param1.Controls.Add(this.btn_Query);
            this.Panel_Param1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.Panel_Param1.HeadHeight = 33;
            this.Panel_Param1.Location = new System.Drawing.Point(27, 24);
            this.Panel_Param1.Name = "Panel_Param1";
            this.Panel_Param1.Size = new System.Drawing.Size(976, 725);
            this.Panel_Param1.TabIndex = 18;
            this.Panel_Param1.Text = "日志查询";
            this.Panel_Param1.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Panel_Param1.ThemeForeColor = System.Drawing.Color.Black;
            // 
            // btn_Recently100
            // 
            this.btn_Recently100.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_Recently100.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Recently100.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Recently100.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Recently100.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Recently100.Location = new System.Drawing.Point(816, 61);
            this.btn_Recently100.Name = "btn_Recently100";
            this.btn_Recently100.Size = new System.Drawing.Size(102, 36);
            this.btn_Recently100.TabIndex = 44;
            this.btn_Recently100.Text = "最近100条";
            this.btn_Recently100.UseVisualStyleBackColor = false;
            this.btn_Recently100.Click += new System.EventHandler(this.btn_Recently100_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_Export.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Export.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Export.Location = new System.Drawing.Point(680, 109);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(102, 36);
            this.btn_Export.TabIndex = 43;
            this.btn_Export.Text = "Excel导出";
            this.btn_Export.UseVisualStyleBackColor = false;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_Recently50
            // 
            this.btn_Recently50.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_Recently50.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Recently50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Recently50.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Recently50.Location = new System.Drawing.Point(680, 61);
            this.btn_Recently50.Name = "btn_Recently50";
            this.btn_Recently50.Size = new System.Drawing.Size(102, 36);
            this.btn_Recently50.TabIndex = 43;
            this.btn_Recently50.Text = "最近50条";
            this.btn_Recently50.UseVisualStyleBackColor = false;
            this.btn_Recently50.Click += new System.EventHandler(this.btn_Recently50_Click);
            // 
            // cmb_LogType
            // 
            this.cmb_LogType.FormattingEnabled = true;
            this.cmb_LogType.Location = new System.Drawing.Point(510, 66);
            this.cmb_LogType.Name = "cmb_LogType";
            this.cmb_LogType.Size = new System.Drawing.Size(129, 28);
            this.cmb_LogType.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(411, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 39;
            this.label1.Text = "日志类型：";
            // 
            // dpt_End
            // 
            this.dpt_End.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dpt_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpt_End.Location = new System.Drawing.Point(143, 110);
            this.dpt_End.Name = "dpt_End";
            this.dpt_End.Size = new System.Drawing.Size(197, 27);
            this.dpt_End.TabIndex = 37;
            // 
            // dpt_Start
            // 
            this.dpt_Start.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dpt_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpt_Start.Location = new System.Drawing.Point(143, 65);
            this.dpt_Start.Name = "dpt_Start";
            this.dpt_Start.Size = new System.Drawing.Size(197, 27);
            this.dpt_Start.TabIndex = 38;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label46.Location = new System.Drawing.Point(55, 113);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(90, 21);
            this.label46.TabIndex = 35;
            this.label46.Text = "结束时间：";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label47.Location = new System.Drawing.Point(55, 68);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(90, 21);
            this.label47.TabIndex = 36;
            this.label47.Text = "开始时间：";
            // 
            // dgv_Log
            // 
            this.dgv_Log.AllowUserToAddRows = false;
            this.dgv_Log.AllowUserToDeleteRows = false;
            this.dgv_Log.AllowUserToResizeColumns = false;
            this.dgv_Log.AllowUserToResizeRows = false;
            this.dgv_Log.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(227)))), ((int)(((byte)(248)))));
            this.dgv_Log.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(227)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Log.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Log.ColumnHeadersHeight = 35;
            this.dgv_Log.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LogTime,
            this.LogInfo,
            this.LogState,
            this.LogType});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Log.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Log.EnableHeadersVisualStyles = false;
            this.dgv_Log.GridColor = System.Drawing.Color.Gray;
            this.dgv_Log.Location = new System.Drawing.Point(40, 167);
            this.dgv_Log.Name = "dgv_Log";
            this.dgv_Log.ReadOnly = true;
            this.dgv_Log.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Log.RowHeadersWidth = 50;
            this.dgv_Log.RowTemplate.Height = 30;
            this.dgv_Log.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Log.Size = new System.Drawing.Size(897, 532);
            this.dgv_Log.TabIndex = 12;
            // 
            // LogTime
            // 
            this.LogTime.DataPropertyName = "LogTime";
            this.LogTime.HeaderText = "日志时间";
            this.LogTime.Name = "LogTime";
            this.LogTime.ReadOnly = true;
            this.LogTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LogTime.Width = 200;
            // 
            // LogInfo
            // 
            this.LogInfo.DataPropertyName = "LogInfo";
            this.LogInfo.HeaderText = "日志信息";
            this.LogInfo.Name = "LogInfo";
            this.LogInfo.ReadOnly = true;
            this.LogInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LogInfo.Width = 300;
            // 
            // LogState
            // 
            this.LogState.DataPropertyName = "LogState";
            this.LogState.HeaderText = "日志状态";
            this.LogState.Name = "LogState";
            this.LogState.ReadOnly = true;
            this.LogState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LogState.Width = 160;
            // 
            // LogType
            // 
            this.LogType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LogType.DataPropertyName = "LogType";
            this.LogType.HeaderText = "日志类型";
            this.LogType.Name = "LogType";
            this.LogType.ReadOnly = true;
            this.LogType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btn_Print
            // 
            this.btn_Print.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Print.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Print.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Print.Location = new System.Drawing.Point(816, 109);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(102, 36);
            this.btn_Print.TabIndex = 11;
            this.btn_Print.Text = "数据打印";
            this.btn_Print.UseVisualStyleBackColor = false;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_Clear.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clear.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Clear.Location = new System.Drawing.Point(544, 109);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(102, 36);
            this.btn_Clear.TabIndex = 0;
            this.btn_Clear.Text = "清除数据";
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // FrmSysLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(227)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1034, 769);
            this.Controls.Add(this.Panel_Param1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmSysLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统日志";
            this.Panel_Param1.ResumeLayout(false);
            this.Panel_Param1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Log)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Query;
        private xktControl.xktPlateHead Panel_Param1;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.DateTimePicker dpt_End;
        private System.Windows.Forms.DateTimePicker dpt_Start;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.DataGridView dgv_Log;
        private System.Windows.Forms.Button btn_Recently100;
        private System.Windows.Forms.Button btn_Recently50;
        private System.Windows.Forms.ComboBox cmb_LogType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogState;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogType;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Button btn_Clear;
    }
}