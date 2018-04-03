using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Spire.Xls;

namespace SpireTest
{
    public partial class frmSpireTest : Form
    {
        string filePath = @"C:\Users\Administrator\Downloads\SpireTest.xls";
        public frmSpireTest()
        {
            InitializeComponent();
        }

        private void frmSpireTest_Load(object sender, EventArgs e)
        {
            ExportChart();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportChart();
        }

        private void ExportChart()
        {
            try
            {
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];
                sheet.Range[1, 1].Text = "姓名";
                sheet.Range[1, 2].Text = "语文分数";
                sheet.Range[1, 3].Text = "数学分数";
                sheet.Range[2, 1].Text = "张三";
                sheet.Range[2, 2].NumberValue = 30;
                sheet.Range[2, 3].NumberValue = 39;
                sheet.Range[3, 1].Text = "李四";
                sheet.Range[3, 2].NumberValue = 40;
                sheet.Range[3, 3].NumberValue = 49;

                //列宽 行距
                sheet.Range[1, 1, 100, 100].RowHeight = 20;
                sheet.Range[1, 1, 100, 100].ColumnWidth = 15;
                //字体
                sheet.Range[1, 1, 1, 3].Style.Font.Size = 12;
                sheet.Range[1, 1, 1, 3].Style.Font.IsBold = true;
                //居中
                sheet.Range[1, 1, 3, 3].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet.Range[1, 1, 3, 3].Style.VerticalAlignment = VerticalAlignType.Center;
                //边框
                sheet.Range[1, 1, 3, 3].BorderAround(LineStyleType.Thin);
                sheet.Range[1, 1, 3, 3].BorderInside(LineStyleType.Thin);

                //生成相应图表
                ChartColumn(sheet);
                ChartPie(sheet);

                workbook.SaveToFile(filePath, ExcelVersion.Version97to2003);
                System.Diagnostics.Process.Start( filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        /// <summary>
        /// 柱形图，语文，数学 多个颜色
        /// </summary>
        /// <param name="sheet"></param>
        private void ChartColumn(Worksheet sheet)
        {
            Spire.Xls.Chart chartColumn = sheet.Charts.Add(ExcelChartType.ColumnClustered);
            chartColumn.ChartTitle = "分数";
            chartColumn.ChartTitleArea.IsBold = true;
            chartColumn.ChartTitleArea.Size = 15;
            chartColumn.DataRange = sheet.Range[2, 2, 3, 3];
            chartColumn.SeriesDataFromRange = false;
            chartColumn.LeftColumn = 1;
            chartColumn.TopRow = 5;
            chartColumn.RightColumn = 6;
            chartColumn.BottomRow = 16;
            chartColumn.PrimaryCategoryAxis.Title = "姓名";//X轴显示名
            chartColumn.PrimaryCategoryAxis.CategoryLabels = sheet.Range[2, 1, 3, 1];
            chartColumn.PrimaryValueAxis.Title = "分数";//Y轴显示名
            chartColumn.Legend.Position = LegendPositionType.Right;
            chartColumn.PlotArea.ForeGroundColor = Color.Transparent;//透明，去掉默认的灰色

            #region 初始化图例
            Spire.Xls.Charts.ChartSerie csColumn1 = chartColumn.Series[0];
            csColumn1.Name = "语文分数";
            csColumn1.Values = sheet.Range[2, 2, 3, 2];
            //csColumn.Values.NumberFormat = "0.0#%";
            csColumn1.DataPoints.DefaultDataPoint.DataLabels.HasValue = true;
            csColumn1.Format.Fill.ForeColor = ColorTranslator.FromHtml("#5B9BD5");

            Spire.Xls.Charts.ChartSerie csColumn2 = chartColumn.Series[1];
            csColumn2.Name = "数学分数";
            csColumn2.Values = sheet.Range[2, 3, 3, 3];
            csColumn2.DataPoints.DefaultDataPoint.DataLabels.HasValue = true;
            csColumn2.Format.Fill.ForeColor = ColorTranslator.FromHtml("#ED7D31");
            #endregion
        }

        /// <summary>
        /// 用姓名+语文成绩 生成饼图
        /// </summary>
        /// <param name="sheet"></param>
        private void ChartPie(Worksheet sheet)
        {
            Spire.Xls.Chart chartPie = sheet.Charts.Add(ExcelChartType.Pie);
            chartPie.DataRange = sheet.Range[2, 2, 3, 2];
            chartPie.SeriesDataFromRange = false;
            //设置图表的位置
            chartPie.LeftColumn = 1;
            chartPie.TopRow = 17;
            chartPie.RightColumn = 6;
            chartPie.BottomRow = 27;
            //图表标题
            chartPie.ChartTitle = "语文成绩";
            //设置字体
            chartPie.ChartTitleArea.IsBold = true;
            chartPie.ChartTitleArea.Size = 12;

            #region 初始化图例
            Spire.Xls.Charts.ChartSerie csPie = chartPie.Series[0];
            csPie.CategoryLabels = sheet.Range[2, 1, 3, 1];
            csPie.Values = sheet.Range[2, 2,3,2];

            csPie.DataPoints[0].DataFormat.Fill.ForeColor = Color.FromArgb(91, 155, 213);
            csPie.DataPoints[1].DataFormat.Fill.ForeColor = ColorTranslator.FromHtml("#ED7D31");
            #endregion
        }
    }
}