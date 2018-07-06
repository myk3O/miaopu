using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
  public   class HighCharts
  {
      private string title;
      /// <summary>
      /// 图标标题
      /// </summary>
      public string Title
      {
          get { return title; }
          set { title = value; }
      }
      private string xAxis;
      /// <summary>
      /// 图标横坐标
      /// </summary>
      public string XAxis
      {
          get { return xAxis; }
          set { xAxis = value; }
      }
      private string yAxis;
      /// <summary>
      /// 图标纵坐标
      /// </summary>
      public string YAxis
      {
          get { return yAxis; }
          set { yAxis = value; }
      }
      private string toolTip;
      /// <summary>
      /// 图标显示的提示信息
      /// </summary>
      public string ToolTip
      {
          get { return toolTip; }
          set { toolTip = value; }
      }
      private string seriesData;
      /// <summary>
      /// 图表数据
      /// </summary>
      public string SeriesData
      {
          get { return seriesData; }
          set { seriesData = value; }
      }
    }
}
