namespace thinger.cn.BMSPro
{
    partial class FrmParamSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParamSet));
            this.txt_SetValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lbl_VarName = new System.Windows.Forms.Label();
            this.lbl_VarType = new System.Windows.Forms.Label();
            this.lbl_VarValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_SetValue
            // 
            this.txt_SetValue.Location = new System.Drawing.Point(138, 164);
            this.txt_SetValue.Name = "txt_SetValue";
            this.txt_SetValue.Size = new System.Drawing.Size(193, 26);
            this.txt_SetValue.TabIndex = 0;
            this.txt_SetValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_SetValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_SetValue_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 168);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "修改数值：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 124);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "当前数值：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "变量名称：";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Cancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Cancel.Location = new System.Drawing.Point(220, 219);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(90, 35);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_OK.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OK.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_OK.Location = new System.Drawing.Point(79, 219);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(90, 35);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // lbl_VarName
            // 
            this.lbl_VarName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_VarName.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.lbl_VarName.Location = new System.Drawing.Point(138, 35);
            this.lbl_VarName.Name = "lbl_VarName";
            this.lbl_VarName.Size = new System.Drawing.Size(193, 26);
            this.lbl_VarName.TabIndex = 22;
            this.lbl_VarName.Tag = "";
            this.lbl_VarName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_VarType
            // 
            this.lbl_VarType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_VarType.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.lbl_VarType.Location = new System.Drawing.Point(138, 121);
            this.lbl_VarType.Name = "lbl_VarType";
            this.lbl_VarType.Size = new System.Drawing.Size(193, 26);
            this.lbl_VarType.TabIndex = 21;
            this.lbl_VarType.Tag = "";
            this.lbl_VarType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_VarValue
            // 
            this.lbl_VarValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_VarValue.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.lbl_VarValue.Location = new System.Drawing.Point(138, 78);
            this.lbl_VarValue.Name = "lbl_VarValue";
            this.lbl_VarValue.Size = new System.Drawing.Size(193, 26);
            this.lbl_VarValue.TabIndex = 20;
            this.lbl_VarValue.Tag = "";
            this.lbl_VarValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmParamSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(387, 283);
            this.Controls.Add(this.lbl_VarName);
            this.Controls.Add(this.lbl_VarType);
            this.Controls.Add(this.lbl_VarValue);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.txt_SetValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmParamSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数修改";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_SetValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label lbl_VarName;
        private System.Windows.Forms.Label lbl_VarType;
        private System.Windows.Forms.Label lbl_VarValue;
    }
}