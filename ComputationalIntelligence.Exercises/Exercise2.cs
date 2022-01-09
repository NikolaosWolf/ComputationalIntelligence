using ComputationalIntelligence.Core.Extras;
using ComputationalIntelligence.Core.Models;
using ComputationalIntelligence.DataSets;
using ComputationalIntelligence.DataSets.Models;
using ComputationalIntelligence.Exercises.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace ComputationalIntelligence.Exercises
{
    public class Exercise2 : IExercise
    {
        public void Execute()
        {
            var pointResult = CreatePoints();

            var clusters = ExecuteKMeans(pointResult.Points);

            foreach (var cluster in clusters)
            {
                Console.WriteLine($"{cluster.Center.X1}, {cluster.Center.X2}");
            }

            var dataSet = CreateDataSet(clusters.SelectMany(c => c.Points).ToHashSet());
            CreateChart(dataSet);
        }

        private PointResult CreatePoints()
        {
            var generator = new PointGenerator();
            var points = generator.Generate();

            return points;
        }

        private List<Cluster> ExecuteKMeans(ISet<DataSets.Models.Point> totalPoints)
        {
            var clusters = CreateClusters(totalPoints, Settings2.M);

            foreach (var point in totalPoints)
            {
                var cluster = FindClusterWithMinimumDistance(clusters, point);
                cluster.Points.Add(point);
            }

            return clusters;
        }

        private List<Cluster> CreateClusters(ISet<DataSets.Models.Point> totalPoints, int numberOfClusters)
        {
            var rnd = new Random();

            var clusters = new List<Cluster>(numberOfClusters);
            for (int i = 0; i < numberOfClusters; i++)
                clusters.Add(CreateCluster(totalPoints, rnd));

            return clusters;
        }

        private Cluster CreateCluster(ISet<DataSets.Models.Point> totalPoints, Random rnd)
        {
            return new Cluster
            {
                Center = totalPoints.ElementAt(rnd.Next(1201))
            };
        }

        private Cluster FindClusterWithMinimumDistance(List<Cluster> clusters, DataSets.Models.Point point)
        {
            Cluster result = null;
            double distance = 2;

            foreach (var cluster in clusters)
            {
                double currentDistance = Functions.EuclideanDistance(cluster.Center.X1, cluster.Center.X2, point.X1, point.X2);

                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    result = cluster;
                }
            }

            return result;
        }

        private DataSet CreateDataSet(ISet<DataSets.Models.Point> totalPoints)
        {
            var dataSet = new DataSet();
            var dt = new DataTable();

            dt.Columns.Add("X1", typeof(double));
            dt.Columns.Add("X2", typeof(double));

            AddRows(dt, totalPoints);

            dataSet.Tables.Add(dt);

            return dataSet;
        }

        private void AddRows(DataTable dataTable, ISet<DataSets.Models.Point> points)
        {
            foreach (var point in points)
            {
                DataRow row = dataTable.NewRow();
                row[0] = point.X1;
                row[1] = point.X2;

                dataTable.Rows.Add(row);
            }
        }

        private void CreateChart(DataSet dataSet)
        {
            Chart chart = new Chart();
            chart.DataSource = dataSet.Tables[0];
            chart.Width = 1280;
            chart.Height = 1024;

            Series serie1 = new Series();
            serie1.Name = "Serie1";
            serie1.Color = Color.FromArgb(112, 255, 200);
            serie1.BorderColor = Color.FromArgb(164, 164, 164);
            serie1.ChartType = SeriesChartType.Point;
            serie1.BorderDashStyle = ChartDashStyle.Solid;
            serie1.BorderWidth = 1;
            serie1.ShadowColor = Color.FromArgb(128, 128, 128);
            serie1.ShadowOffset = 1;
            serie1.IsValueShownAsLabel = false;
            serie1.XValueMember = "X1";
            serie1.YValueMembers = "X2";
            serie1.Font = new Font("Tahoma", 8.0f);
            serie1.BackSecondaryColor = Color.FromArgb(0, 102, 153);
            serie1.LabelForeColor = Color.FromArgb(100, 100, 100);
            chart.Series.Add(serie1);

            ChartArea ca = new ChartArea();
            ca.Name = "ChartArea1";
            ca.BackColor = Color.White;
            ca.BorderColor = Color.FromArgb(26, 59, 105);
            ca.BorderWidth = 0;
            ca.BorderDashStyle = ChartDashStyle.Solid;
            ca.AxisX = new Axis() { Title = "X1"};
            ca.AxisY = new Axis() { Title = "X2" };
            chart.ChartAreas.Add(ca);

            chart.DataBind();

            chart.SaveImage(Settings2.OutputImagePath, ChartImageFormat.Png);
        }
    }
}
