using ComputationalIntelligence.Core.Models;
using System;
using System.Collections.Generic;

namespace ComputationalIntelligence.DataSets
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

        private void AddPoints(ISet<Point> points, int size, double rangeX1, double offsetX1, double rangeX2, double offsetX2, Random rnd)
        {
            for (int i = 0; i < size; i++)
            {
                points.Add(new Point
                {
                    X1 = (rnd.NextDouble() * rangeX1) + offsetX1,
                    X2 = (rnd.NextDouble() * rangeX2) + offsetX2
                });
            }
        }
    }
}
