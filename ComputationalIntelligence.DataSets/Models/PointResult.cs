using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalIntelligence.DataSets.Models
{
    public class PointResult
    {
        public PointResult()
        {
            Square1 = new HashSet<Point>();
            Square2 = new HashSet<Point>();
            Square3 = new HashSet<Point>();
            Square4 = new HashSet<Point>();
            Square5 = new HashSet<Point>();
            Square6 = new HashSet<Point>();
            Square7 = new HashSet<Point>();
            Square8 = new HashSet<Point>();
            Square9 = new HashSet<Point>();
            Square10 = new HashSet<Point>();
        }

        public ISet<Point> Square1 { get; set; }

        public ISet<Point> Square2 { get; set; }

        public ISet<Point> Square3 { get; set; }

        public ISet<Point> Square4 { get; set; }

        public ISet<Point> Square5 { get; set; }

        public ISet<Point> Square6 { get; set; }

        public ISet<Point> Square7 { get; set; }

        public ISet<Point> Square8 { get; set; }

        public ISet<Point> Square9 { get; set; }

        public ISet<Point> Square10 { get; set; }
    }
}
