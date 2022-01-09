using ComputationalIntelligence.DataSets.Models;
using System;
using System.Collections.Generic;

namespace ComputationalIntelligence.DataSets
{
    public class PointGenerator
    {
        public PointResult Generate()
        {
            var rnd = new Random();

            return new PointResult
            {
                Square1 = GenerateSquarePoints(rnd, 150, 0.5, 0.75, 0.5, 0.75),
                Square2 = GenerateSquarePoints(rnd, 150, 0.5, 0.00, 0.5, 0.00),
                Square3 = GenerateSquarePoints(rnd, 150, 0.5, 0.00, 0.5, 1.50),
                Square4 = GenerateSquarePoints(rnd, 150, 0.5, 1.50, 0.5, 0.00),
                Square5 = GenerateSquarePoints(rnd, 150, 0.5, 1.50, 0.5, 1.50),
                Square6 = GenerateSquarePoints(rnd,  75, 0.2, 0.60, 0.4, 0.00),
                Square7 = GenerateSquarePoints(rnd,  75, 0.2, 0.60, 0.4, 1.60),
                Square8 = GenerateSquarePoints(rnd,  75, 0.2, 1.20, 0.4, 0.00),
                Square9 = GenerateSquarePoints(rnd,  75, 0.2, 1.20, 0.4, 1.60),
                Square10 = GenerateSquarePoints(rnd,150, 2.0, 0.00, 2.0, 0.00)
            };
        }

        private ISet<Point> GenerateSquarePoints(Random rnd, int size, double rangeX1, double offsetX1, double rangeX2, double offsetX2)
        {
            var squarePoints = new HashSet<Point>(size);

            for (int i = 0; i < size; i++)
            {
                squarePoints.Add(new Point
                {
                    X1 = (rnd.NextDouble() * rangeX1) + offsetX1,
                    X2 = (rnd.NextDouble() * rangeX2) + offsetX2
                });
            }

            return squarePoints;
        }
    }
}
