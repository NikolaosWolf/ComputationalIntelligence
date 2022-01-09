using System.Collections.Generic;

namespace ComputationalIntelligence.DataSets.Models
{
    public class ExampleResult
    {
        public ExampleResult()
        {
            TrainingSet = new HashSet<Example>();
            TestSet = new HashSet<Example>();
        }

        public ISet<Example> TrainingSet { get; set; }

        public ISet<Example> TestSet { get; set; }
    }
}
