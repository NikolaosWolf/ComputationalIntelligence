using ComputationalIntelligence.Core.Extras;
using ComputationalIntelligence.Core.Models;
using ComputationalIntelligence.DataSets;
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
            var points = CreatePoints();

            var clusters = ExecuteKMeans(points);

            int imageId = 1;
            double clusteringError = 0;
            foreach (var cluster in clusters)
            {
                var dataSet = CreateDataSet(cluster.Points);
                CreateChart(dataSet, imageId);
                imageId++;

                clusteringError += cluster.Points.Sum(p => Functions.EuclideanDistance(cluster.Center.X1, cluster.Center.X2, p.X1, p.X2));
            }

            Console.WriteLine($"Clustering Error: {Math.Round(clusteringError, 2)}");
        }

        private ISet<Core.Models.Point> CreatePoints()
        {
            var generator = new PointGenerator();
            var points = generator.Generate();

            return points;
        }

        private ISet<Cluster> ExecuteKMeans(ISet<Core.Models.Point> totalPoints)
        {
            var clusters = InitializeClusters(totalPoints, Settings2.M);

            int count = 0;
            bool changed = true;

            while (changed == true && count < Settings2.SanityCheck)
            {
                UpdateClustering(totalPoints, clusters);
                changed = UpdateCenters(clusters);

                count++;
            }

            return clusters;
        }

        private ISet<Cluster> InitializeClusters(ISet<Core.Models.Point> totalPoints, int numberOfClusters)
        {
            var clusters = new HashSet<Cluster>(numberOfClusters);

            var existingCenters = new HashSet<Core.Models.Point>(numberOfClusters);

            for (int i = 0; i < numberOfClusters; i++)
            {
                clusters.Add(new Cluster
                {
                    Id = i + 1,
                    Center = InitializeCenter(totalPoints, existingCenters)
                });
            }

            return clusters;
        }

        private Core.Models.Point InitializeCenter(ISet<Core.Models.Point> totalPoints, ISet<Core.Models.Point> existingCenters)
        {
            var rnd = new Random();

            var center = totalPoints.ElementAt(rnd.Next(1200));

            while (existingCenters.Contains(center))
                center = totalPoints.ElementAt(rnd.Next(1200));

            existingCenters.Add(center);

            return center;
        }

        private void UpdateClustering(ISet<Core.Models.Point> totalPoints, ISet<Cluster> clusters)
        {
            foreach (var point in totalPoints)
            {
                var distances = CalculateDistances(point, clusters);
                var minimumDistance = distances.Single(d => d.Distance == distances.Min(d => d.Distance));

                if (point.Cluster == null || point.Cluster.Id != minimumDistance.ClusterId)
                {
                    if (point.Cluster != null)
                        clusters.Single(c => c.Id == point.Cluster.Id).Points.Remove(point);

                    clusters.Single(c => c.Id == minimumDistance.ClusterId).Points.Add(point);
                }

            }
        }

        private List<(int ClusterId, double Distance)> CalculateDistances(Core.Models.Point point, ISet<Cluster> clusters)
        {
            var distances = new List<(int, double)>(clusters.Count());

            foreach (var cluster in clusters)
                distances.Add((cluster.Id, Functions.EuclideanDistance(cluster.Center.X1, cluster.Center.X2, point.X1, point.X2)));

            return distances;
        }

        private bool UpdateCenters(ISet<Cluster> clusters)
        {
            bool changed = false;

            foreach (var cluster in clusters)
            {
                var meanX1 = cluster.Points.Sum(p => p.X1) / cluster.Points.Count();
                var meanX2 = cluster.Points.Sum(p => p.X2) / cluster.Points.Count();

                if (cluster.Center.X1 != meanX1)
                {
                    cluster.Center.X1 = meanX1;
                    changed = true;
                }

                if (cluster.Center.X2 != meanX2)
                {
                    cluster.Center.X2 = meanX2;
                    changed = true;
                }
            }

            return changed;
        }

        private DataSet CreateDataSet(ISet<Core.Models.Point> totalPoints)
        {
            var dataSet = new DataSet();
            var dt = new DataTable();

            dt.Columns.Add("X1", typeof(double));
            dt.Columns.Add("X2", typeof(double));

            AddRows(dt, totalPoints);

            dataSet.Tables.Add(dt);

            return dataSet;
        }

        private void AddRows(DataTable dataTable, ISet<Core.Models.Point> points)
        {
            foreach (var point in points)
            {
                DataRow row = dataTable.NewRow();
                row[0] = point.X1;
                row[1] = point.X2;

                dataTable.Rows.Add(row);
            }
        }

        private void CreateChart(DataSet dataSet, int id)
        {
            Chart chart = new Chart();
            chart.DataSource = dataSet.Tables[0];
            chart.Width = 1280;
            chart.Height = 1024;

            Series serie1 = new Series();
            serie1.Name = "Serie1";
            serie1.Color = Color.Red;
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
            ca.AxisX = new Axis() { Title = "X1" };
            ca.AxisY = new Axis() { Title = "X2" };
            chart.ChartAreas.Add(ca);

            chart.DataBind();

            chart.SaveImage(Settings2.OutputImagePath + $"{id}" + Settings2.ImageType, ChartImageFormat.Png);
        }
    }
}
