using ComputationalIntelligence.Core.Models;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace ComputationalIntelligence.Charts
{
    public class ChartGenerator
    {
        public void Generate(ISet<Cluster> clusters, string outputFile)
        {
            var chart = new Chart { Width = 1280, Height = 1024 };
            
            ChartArea chartArea = CreateChartArea("ChartArea");
            chart.ChartAreas.Add(chartArea);

            foreach (var cluster in clusters)
            {
                chart.Series.Add(CreateSeries(cluster.Points.Select(p => (p.X1, p.X2)), $"Cluster{cluster.Id}", Settings.Colors[cluster.Id]));
            }

            chart.Series.Add(CreateSeries(clusters.Select(c => (c.Center.X1, c.Center.X2)), $"Centers", Color.Black));

            chart.Titles.Add("K-Means");

            chart.SaveImage(outputFile, ChartImageFormat.Png);
        }

        private ChartArea CreateChartArea(string areaName)
        {
            var chartArea = new ChartArea();
            chartArea.Name = areaName;
            chartArea.BackColor = Color.White;
            chartArea.BorderColor = Color.FromArgb(26, 59, 105);
            chartArea.BorderWidth = 0;
            chartArea.BorderDashStyle = ChartDashStyle.Solid;

            chartArea.AxisX = new Axis() { Title = "X1" };
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 2;

            chartArea.AxisY = new Axis() { Title = "X2" };
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 2;

            return chartArea;
        }

        private Series CreateSeries(IEnumerable<(double X, double Y)> points, string seriesName, Color color)
        {
            var series = new Series();
            series.Name = seriesName;
            series.Color = color;

            series.BorderColor = Color.FromArgb(164, 164, 164);
            series.ChartType = SeriesChartType.Point;
            series.BorderDashStyle = ChartDashStyle.Solid;
            series.BorderWidth = 1;
            series.ShadowColor = Color.FromArgb(128, 128, 128);
            series.ShadowOffset = 1;
            series.IsValueShownAsLabel = false;
            series.XValueMember = "X1";
            series.YValueMembers = "X2";
            series.Font = new Font("Tahoma", 8.0f);
            series.BackSecondaryColor = Color.FromArgb(0, 102, 153);
            series.LabelForeColor = Color.FromArgb(100, 100, 100);

            foreach (var point in points)
                series.Points.AddXY(point.X, point.Y);

            return series;
        }
    }
}
