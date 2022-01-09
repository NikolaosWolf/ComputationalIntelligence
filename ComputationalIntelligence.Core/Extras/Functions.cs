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
    }
}
