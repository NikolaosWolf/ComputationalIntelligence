using Helpers;
using KMeans.Models;

namespace KMeans.Code
{
    public class KMeansAlgorithm
    {
        public ISet<Cluster> Execute(ISet<Point> totalPoints)
        {
            var clusters = InitializeClusters(totalPoints, Settings.M);

            int count = 0;
            bool changed = true;

            while (changed && count < Settings.SanityCheck)
            {
                UpdateClustering(totalPoints, clusters);
                changed = UpdateCenters(clusters);

                count++;
            }

            return clusters;
        }

        private ISet<Cluster> InitializeClusters(ISet<Point> totalPoints, int numberOfClusters)
        {
            var clusters = new HashSet<Cluster>(numberOfClusters);

            var existingCenters = new HashSet<Point>(numberOfClusters);

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

        private Point InitializeCenter(ISet<Point> totalPoints, ISet<Point> existingCenters)
        {
            var rnd = new Random();

            var center = totalPoints.ElementAt(rnd.Next(1200));

            while (existingCenters.Contains(center))
                center = totalPoints.ElementAt(rnd.Next(1200));

            existingCenters.Add(center);

            return center;
        }

        private void UpdateClustering(ISet<Point> totalPoints, ISet<Cluster> clusters)
        {
            foreach (var point in totalPoints)
            {
                var distances = CalculateDistances(point, clusters);
                var minimumDistance = distances.Single(d => d.Distance == distances.Min(d => d.Distance));

                if (point.Cluster == null || point.Cluster.Id != minimumDistance.ClusterId)
                {
                    if (point.Cluster != null)
                        clusters.Single(c => c.Id == point.Cluster.Id).Points.Remove(point);

                    var cluster = clusters.Single(c => c.Id == minimumDistance.ClusterId);

                    point.Cluster = cluster;
                    cluster.Points.Add(point);
                }

            }
        }

        private List<(int ClusterId, double Distance)> CalculateDistances(Point point, ISet<Cluster> clusters)
        {
            var distances = new List<(int, double)>(clusters.Count());

            foreach (var cluster in clusters)
                distances.Add((cluster.Id, Functions.EuclideanDistance(cluster.Center.X, cluster.Center.Y, point.X, point.Y)));

            return distances;
        }

        private bool UpdateCenters(ISet<Cluster> clusters)
        {
            bool changed = false;

            foreach (var cluster in clusters)
            {
                var meanX1 = cluster.Points.Sum(p => p.X) / cluster.Points.Count();
                var meanX2 = cluster.Points.Sum(p => p.Y) / cluster.Points.Count();

                if (cluster.Center.X != meanX1)
                {
                    cluster.Center.X = meanX1;
                    changed = true;
                }

                if (cluster.Center.Y != meanX2)
                {
                    cluster.Center.Y = meanX2;
                    changed = true;
                }
            }

            return changed;
        }
    }
}
