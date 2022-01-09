using ComputationalIntelligence.DataSets.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalIntelligence.DataSets
{
    public interface IGenerator
    {
        ExampleResult Generate();
    }
}
