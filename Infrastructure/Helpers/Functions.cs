namespace Helpers
{
    public static class Functions
    {
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
            var x = Math.Pow(x1 - x2, 2);
            var y = Math.Pow(y1 - y2, 2);

            return Math.Sqrt(x + y);
        }
    }
}