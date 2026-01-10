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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thinger.cn.BMSControl
{
    public enum Language
    {
        Chinese,
        English
    }
    public enum VoltageClass
    {
        Max,
        Normal,
        Min
    }

    public partial class CellVoltage : UserControl
    {
        public CellVoltage()
        {
            InitializeComponent();
        }

        private int index = 1;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
            }
        }

        private Language myLang=Language.Chinese;

        public Language MyLang
        {
            get { return myLang; }
            set
            {
                myLang = value;
                this.lbl_Title.Text = myLang == Language.Chinese? "第" + index + "串":"Cell "+index;
            }
        }


        private string cellVolValue;

        public string CellVolValue
        {
            get { return cellVolValue; }
            set
            {
                cellVolValue = value;
                this.lbl_Value.Text = cellVolValue;
            }
        }

        private VoltageClass voltageClass = VoltageClass.Normal;

        public VoltageClass VoltageClass
        {
            get { return voltageClass; }
            set { voltageClass = value;
                switch (voltageClass)
                {
                    case VoltageClass.Max:
                        this.lbl_Title.ForeColor = Color.Red;
                        break;
                    case VoltageClass.Normal:
                        this.lbl_Title.ForeColor = SystemColors.ControlText;
                        break;
                    case VoltageClass.Min:
                        this.lbl_Title.ForeColor = Color.Blue;
                        break;
                    default:
                        this.lbl_Title.ForeColor = SystemColors.ControlText;
                        break;
                }

            }
        }

    }
}
