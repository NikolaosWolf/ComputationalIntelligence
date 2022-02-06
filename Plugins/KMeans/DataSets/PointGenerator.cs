using KMeans.Models;

namespace KMeans.DataSets
{
    public class PointGenerator
    {
        public ISet<Point> Generate()
        {
            var rnd = new Random();

            var points = new HashSet<Point>(1200);

            AddPoints(points, 150, 0.5, 0.75, 0.5, 0.75, rnd);
            AddPoints(points, 150, 0.5, 0.00, 0.5, 0.00, rnd);
            AddPoints(points, 150, 0.5, 0.00, 0.5, 1.50, rnd);
            AddPoints(points, 150, 0.5, 1.50, 0.5, 0.00, rnd);
            AddPoints(points, 150, 0.5, 1.50, 0.5, 1.50, rnd);
            AddPoints(points,  75, 0.2, 0.60, 0.4, 0.00, rnd);
            AddPoints(points,  75, 0.2, 0.60, 0.4, 1.60, rnd);
            AddPoints(points,  75, 0.2, 1.20, 0.4, 0.00, rnd);
            AddPoints(points,  75, 0.2, 1.20, 0.4, 1.60, rnd);
            AddPoints(points, 150, 2.0, 0.00, 2.0, 0.00, rnd);


            return points;
        }

        private void AddPoints(ISet<Point> points, int size, double rangeX, double offsetX, double rangeY, double offsetY, Random rnd)
        {
            for (int i = 0; i < size; i++)
            {
                points.Add(new Point
                {
                    X = (rnd.NextDouble() * rangeX) + offsetX,
                    Y = (rnd.NextDouble() * rangeY) + offsetY
                });
            }
        }
    }
}
