using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalIntelligence.Core.Models
{
    public class Layer
    {
        public Layer(int numberOfNeurons, int numberOfInputs)
        {
            Neurons = new List<Neuron>();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                Neurons.Add(new Neuron(numberOfInputs));
            }
        }

        public List<Neuron> Neurons { get; set; }
    }
}
