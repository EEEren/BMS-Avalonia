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
using System.Drawing.Drawing2D;

namespace thinger.cn.BMSControl
{
    /// <summary>
    /// Class UCProcessEllipse.
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class ProcessEllipse : UserControl
    {
        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        [Description("值改变事件"), Category("自定义属性")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// The m back ellipse color
        /// </summary>
        private Color m_backEllipseColor = Color.FromArgb(228, 231, 237);
        /// <summary>
        /// 圆背景色
        /// </summary>
        /// <value>The color of the back ellipse.</value>
        [Description("圆背景色"), Category("自定义属性")]
        public Color BackEllipseColor
        {
            get { return m_backEllipseColor; }
            set
            {
                m_backEllipseColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m core ellipse color
        /// </summary>
        private Color m_coreEllipseColor = Color.FromArgb(228, 231, 237);
        /// <summary>
        /// 内圆颜色，ShowType=Ring 有效
        /// </summary>
        /// <value>The color of the core ellipse.</value>
        [Description("内圆颜色，ShowType=Ring 有效"), Category("自定义属性")]
        public Color CoreEllipseColor
        {
            get { return m_coreEllipseColor; }
            set
            {
                m_coreEllipseColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m value color
        /// </summary>
        private Color m_valueColor = Color.FromArgb(255, 77, 59);

        /// <summary>
        /// Gets or sets the color of the value.
        /// </summary>
        /// <value>The color of the value.</value>
        [Description("值圆颜色"), Category("自定义属性")]
        public Color ValueColor
        {
            get { return m_valueColor; }
            set
            {
                m_valueColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m is show core ellipse border
        /// </summary>
        private bool m_isShowCoreEllipseBorder = true;
        /// <summary>
        /// 内圆是否显示边框，ShowType=Ring 有效
        /// </summary>
        /// <value><c>true</c> if this instance is show core ellipse border; otherwise, <c>false</c>.</value>
        [Description("内圆是否显示边框，ShowType=Ring 有效"), Category("自定义属性")]
        public bool IsShowCoreEllipseBorder
        {
            get { return m_isShowCoreEllipseBorder; }
            set
            {
                m_isShowCoreEllipseBorder = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m value type
        /// </summary>
        private ValueType m_valueType = ValueType.Percent;
        /// <summary>
        /// 值文字类型
        /// </summary>
        /// <value>The type of the value.</value>
        [Description("值文字类型"), Category("自定义属性")]
        public ValueType ValueType
        {
            get { return m_valueType; }
            set
            {
                m_valueType = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m value width
        /// </summary>
        private int m_valueWidth = 30;
        /// <summary>
        /// 外圆值宽度
        /// </summary>
        /// <value>The width of the value.</value>
        [Description("外圆值宽度，ShowType=Ring 有效"), Category("自定义属性")]
        public int ValueWidth
        {
            get { return m_valueWidth; }
            set
            {
                if (value <= 0 || value > Math.Min(this.Width, this.Height))
                    return;
                m_valueWidth = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m value margin
        /// </summary>
        private int m_valueMargin = 5;
        /// <summary>
        /// 外圆值间距
        /// </summary>
        /// <value>The value margin.</value>
        [Description("外圆值间距"), Category("自定义属性")]
        public int ValueMargin
        {
            get { return m_valueMargin; }
            set
            {
                if (value < 0 || m_valueMargin >= m_valueWidth)
                    return;
                m_valueMargin = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m maximum value
        /// </summary>
        private int m_maxValue = 100;
        /// <summary>
        /// 最大值
        /// </summary>
        /// <value>The maximum value.</value>
        [Description("最大值"), Category("自定义属性")]
        public int MaxValue
        {
            get { return m_maxValue; }
            set
            {
                if (value > m_value || value <= 0)
                    return;
                m_maxValue = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m value
        /// </summary>
        private int m_value = 0;
        /// <summary>
        /// 当前值
        /// </summary>
        /// <value>The value.</value>
        [Description("当前值"), Category("自定义属性")]
        public int Value
        {
            get { return m_value; }
            set
            {
                if (m_maxValue < value || value < 0)
                    return;
                m_value = value;
                if (ValueChanged != null)
                {
                    ValueChanged(this, null);
                }
                Refresh();
            }
        }
        /// <summary>
        /// The m font
        /// </summary>

        /// <summary>
        /// 获取或设置控件显示的文字的字体。
        /// </summary>
        /// <value>The font.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Description("文字字体"), Category("自定义属性")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                Refresh();
            }
        }
        /// <summary>
        /// 获取或设置控件的前景色。
        /// </summary>
        /// <value>The color of the fore.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Description("文字颜色"), Category("自定义属性"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Localizable(true)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m show type
        /// </summary>
        private ShowType m_showType = ShowType.Ring;

        /// <summary>
        /// Gets or sets the type of the show.
        /// </summary>
        /// <value>The type of the show.</value>
        [Description("显示类型"), Category("自定义属性")]
        public ShowType ShowType
        {
            get { return m_showType; }
            set
            {
                m_showType = value;
                Refresh();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessEllipse" /> class.
        /// </summary>
        public ProcessEllipse()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            ForeColor = Color.White;
            Font = this.Font;
        }

        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g">The g.</param>
        private void SetGDIHigh(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }

        /// <summary>
        /// 引发 <see cref="E:System.Windows.Forms.Control.Paint" /> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.PaintEventArgs" />。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            SetGDIHigh(g);

            int intWidth = Math.Min(this.Size.Width, this.Size.Height) - 1;
            //底圆
            g.FillEllipse(new SolidBrush(m_backEllipseColor), new Rectangle(new Point(0, 0), new Size(intWidth, intWidth)));
            if (m_showType == ShowType.Ring)
            {
                //中心圆
                int intCore = intWidth - m_valueWidth * 2;
                g.FillEllipse(new SolidBrush(m_coreEllipseColor), new Rectangle(new Point(m_valueWidth, m_valueWidth), new Size(intCore, intCore)));
                //中心圆边框
                if (m_isShowCoreEllipseBorder)
                {
                    g.DrawEllipse(new Pen(m_valueColor, 2), new Rectangle(new Point(m_valueWidth + 1, m_valueWidth + 1), new Size(intCore - 1, intCore - 1)));
                }
                if (m_value > 0 && m_maxValue > 0)
                {
                    float fltPercent = (float)m_value / (float)m_maxValue;
                    if (fltPercent > 1)
                    {
                        fltPercent = 1;
                    }

                    g.DrawArc(new Pen(m_valueColor, m_valueWidth - m_valueMargin * 2), new RectangleF(new Point(m_valueWidth / 2 + m_valueMargin / 4, m_valueWidth / 2 + m_valueMargin / 4), new SizeF(intWidth - m_valueWidth - m_valueMargin / 2 + (m_valueMargin == 0 ? 0 : 1), intWidth - m_valueWidth - m_valueMargin / 2 + (m_valueMargin == 0 ? 0 : 1))), -90, fltPercent * 360);

                    string strValueText = m_valueType == ValueType.Percent ? fltPercent.ToString("0%") : m_value.ToString();
                    System.Drawing.SizeF _txtSize = g.MeasureString(strValueText, this.Font);
                    g.DrawString(strValueText, this.Font, new SolidBrush(this.ForeColor), new PointF((intWidth - _txtSize.Width) / 2 + 1, (intWidth - _txtSize.Height) / 2 + 1));
                }
            }
            else
            {
                if (m_value > 0 && m_maxValue > 0)
                {
                    float fltPercent = (float)m_value / (float)m_maxValue;
                    if (fltPercent > 1)
                    {
                        fltPercent = 1;
                    }

                    g.FillPie(new SolidBrush(m_valueColor), new Rectangle(m_valueMargin, m_valueMargin, intWidth - m_valueMargin * 2, intWidth - m_valueMargin * 2), -90, fltPercent * 360);

                    string strValueText = m_valueType == ValueType.Percent ? fltPercent.ToString("0%") : m_value.ToString();
                    System.Drawing.SizeF _txtSize = g.MeasureString(strValueText, this.Font);
                    g.DrawString(strValueText, this.Font, new SolidBrush(this.ForeColor), new PointF((intWidth - _txtSize.Width) / 2 + 1, (intWidth - _txtSize.Height) / 2 + 1));
                }
            }

        }
    }

    /// <summary>
    /// Enum ValueType
    /// </summary>
    public enum ValueType
    {
        /// <summary>
        /// 百分比
        /// </summary>
        Percent,
        /// <summary>
        /// 数值
        /// </summary>
        Absolute
    }

    /// <summary>
    /// Enum ShowType
    /// </summary>
    public enum ShowType
    {
        /// <summary>
        /// 圆环
        /// </summary>
        Ring,
        /// <summary>
        /// 扇形
        /// </summary>
        Sector
    }
}
