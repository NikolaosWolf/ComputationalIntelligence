using System.Collections.Generic;

namespace ComputationalIntelligence.Core.Models
{
    public class Cluster
    {
        public Cluster()
        {
            Points = new HashSet<Point>();
        }

        public int Id { get; set; }

        public Point Center { get; set; }

        public ISet<Point> Points { get; set; }
    }
}
