namespace thinger.cn.BMSPro
{
    partial class FrmStoreSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoreSet));
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.btn_Set = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_StorePath = new System.Windows.Forms.TextBox();
            this.btn_Select = new System.Windows.Forms.Button();
            this.chk_UseDefault = new System.Windows.Forms.CheckBox();
            this.num_StoreTime = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.num_StoreTime)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label1.Location = new System.Drawing.Point(52, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "存储路径：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label4.Location = new System.Drawing.Point(230, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "存储间隔：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label5.Location = new System.Drawing.Point(405, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "秒";
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
            this.label38.Text = "Tips：配置信息立即生效";
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
            this.btn_Set.Location = new System.Drawing.Point(226, 161);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(97, 36);
            this.btn_Set.TabIndex = 20;
            this.btn_Set.Text = "保存配置";
            this.btn_Set.UseVisualStyleBackColor = false;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Close.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Close.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Close.Location = new System.Drawing.Point(343, 161);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(97, 36);
            this.btn_Close.TabIndex = 19;
            this.btn_Close.Text = "关闭窗体";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_StorePath
            // 
            this.txt_StorePath.Location = new System.Drawing.Point(137, 39);
            this.txt_StorePath.Name = "txt_StorePath";
            this.txt_StorePath.ReadOnly = true;
            this.txt_StorePath.Size = new System.Drawing.Size(252, 23);
            this.txt_StorePath.TabIndex = 21;
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(406, 39);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(35, 24);
            this.btn_Select.TabIndex = 22;
            this.btn_Select.Text = "...";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // chk_UseDefault
            // 
            this.chk_UseDefault.AutoSize = true;
            this.chk_UseDefault.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.chk_UseDefault.Location = new System.Drawing.Point(56, 98);
            this.chk_UseDefault.Name = "chk_UseDefault";
            this.chk_UseDefault.Size = new System.Drawing.Size(112, 24);
            this.chk_UseDefault.TabIndex = 24;
            this.chk_UseDefault.Text = "使用默认路径";
            this.chk_UseDefault.UseVisualStyleBackColor = true;
            // 
            // num_StoreTime
            // 
            this.num_StoreTime.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.num_StoreTime.Location = new System.Drawing.Point(315, 96);
            this.num_StoreTime.Name = "num_StoreTime";
            this.num_StoreTime.Size = new System.Drawing.Size(74, 26);
            this.num_StoreTime.TabIndex = 25;
            // 
            // FrmStoreSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 229);
            this.Controls.Add(this.num_StoreTime);
            this.Controls.Add(this.chk_UseDefault);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.txt_StorePath);
            this.Controls.Add(this.btn_Set);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FrmStoreSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "存储设置";
            ((System.ComponentModel.ISupportInitialize)(this.num_StoreTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txt_StorePath;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.CheckBox chk_UseDefault;
        private System.Windows.Forms.NumericUpDown num_StoreTime;
    }
}