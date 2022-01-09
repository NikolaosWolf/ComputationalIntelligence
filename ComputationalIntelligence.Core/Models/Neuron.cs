using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalIntelligence.Core.Models
{
    public class Neuron
    {
        public Neuron(int numberOfInputs)
        {
            Weights = new double[numberOfInputs];

            var rnd = new Random();
            for (int i = 0; i < numberOfInputs; i++)
            {
                Weights[i] = 10 * rnd.NextDouble() - 5;
            }
        }

        public double[] Weights { get; set; }
    }
}
