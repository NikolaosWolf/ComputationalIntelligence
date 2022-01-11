using ComputationalIntelligence.Charts;
using ComputationalIntelligence.Core.Extras;
using ComputationalIntelligence.Core.Models;
using ComputationalIntelligence.DataSets;
using ComputationalIntelligence.Exercises.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputationalIntelligence.Exercises
{
    public class Exercise2 : IExercise
    {
        public void Execute()
        {
            var points = CreatePoints();
            var clusters = ExecuteKMeans(points);

            double clusteringError = clusters.Sum(c => c.Points.Sum(p => Functions.EuclideanDistance(c.Center.X1, c.Center.X2, p.X1, p.X2)));

            Console.WriteLine($"Clustering Error: {Math.Round(clusteringError, 2)}");

            CreateChart(clusters);
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

        private void CreateChart(ISet<Cluster> clusters)
        {
            var chartGenerator = new ChartGenerator();

            chartGenerator.Generate(clusters, Settings2.OutputImagePath + Settings2.ImageType);
        }
    }
}
