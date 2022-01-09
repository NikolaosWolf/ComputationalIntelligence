using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalIntelligence.DataSets.Models
{
    public class PointResult
    {
        public PointResult()
        {
            Points = new HashSet<Point>();
        }

        public ISet<Point> Points { get; set; }
    }
}
