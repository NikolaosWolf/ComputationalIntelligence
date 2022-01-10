using System;

namespace ComputationalIntelligence.Core.Extras
{
    public static class Functions
    {
        /// <summary>
        /// Calculate the Hyperbolic Tangent of <see cref="{x}"/>.
        /// </summary>
        /// <param name="x">Input</param>
        /// <param name="a">Angle</param>
        /// <returns></returns>
        public static double Tanh(double x, double a)
        {
            return (Math.Pow(Math.E, a * x) - Math.Pow(Math.E, -a * x)) /
                   (Math.Pow(Math.E, a * x) + Math.Pow(Math.E, -a * x));
        }

        /// <summary>
        /// Rectified Linear Unit function.
        /// </summary>
        /// <param name="x">Input</param>
        /// <returns></returns>
        public static double ReLU(double x)
        {
            return Math.Max(0, x);
        }

        /// <summary>
        /// Calculates the Euclidean Distance of two points (x1, y1) and (x2, y2).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double EuclideanDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
