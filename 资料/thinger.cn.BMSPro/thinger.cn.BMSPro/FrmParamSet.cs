// ***********************************************************************
//    Assembly       : 新阁教育
//    Created          : 2020-11-11
// ***********************************************************************
//     Copyright by 新阁教育（天津星阁教育科技有限公司）
//     QQ：        2934008828（付老师）  
//     WeChat：thinger002（付老师）
//     公众号：   dotNet工控上位机
//     哔哩哔哩：dotNet工控上位机
//     知乎：      dotNet工控上位机
//     头条：      dotNet工控上位机
//     视频号：   dotNet工控上位机
//     版权所有，严禁传播
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xktComm;
using xktComm.Common;
using xktNodeSettings;
using xktNodeSettings.Node.Variable;

namespace thinger.cn.BMSPro
{
    public partial class FrmParamSet : Form
    {
        private ModbusRTUVariable var;
        public FrmParamSet(string varName)
        {
            InitializeComponent();

            //通过变量名称获取到对象
            var = CommonMethods.CurrentRTUVarList[varName];

            this.lbl_VarName.Text = var.Description;
            this.lbl_VarValue.Text = var.Value?.ToString();
            this.lbl_VarType.Text = var.VarType.ToString();

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            XktResult<string> result = VerifyInputValue(var.Alarm, this.txt_SetValue.Text.Trim(), var.VarType, var.Scale, var.Offset);

            if (result.IsSuccess)
            {
                if (CommonMethods.CommonWrite(var, this.txt_SetValue.Text.Trim()))
                {
                    MessageBox.Show("参数设置成功", "参数设置");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("参数设置失败", "参数设置");
                }
            }
            else
            {
                MessageBox.Show(result.Message, "参数错误");
            }
        }

        private XktResult<string> VerifyInputValue(AlarmEnitity alarm, string set, DataType type, string scale, string offset)
        {
            XktResult<string> result = new XktResult<string>();
            //非空
            if (set.Length == 0)
            {
                result.IsSuccess = false;
                result.Message = "参数错误：设定值不能为空";
                return result;
            }

            //设定限值
            if (alarm.SetLimitEnable)
            {
                float value = 0.0f;

                if (float.TryParse(set, out value))
                {
                    if (value > alarm.SetLimitMax || value < alarm.SetLimitMin)
                    {
                        result.IsSuccess = false;
                        result.Message = "参数错误：不在设定范围内";
                        return result;
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "参数错误：设定值格式不正确";
                    return result;
                }
            }
            //转换验证
            return MigrationLib.SetMigrationValue(set, type, scale, offset);
        }

        private void txt_SetValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btn_OK_Click(null, null);
            }
        }
    }
}
