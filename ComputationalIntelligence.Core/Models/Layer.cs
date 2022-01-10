using ComputationalIntelligence.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationalIntelligence.Core.Models
{
    public class Layer
    {
        public Layer(int numberOfNeurons, int numberOfInputs, ActivationFunction activationFunction)
        {
            Neurons = new List<Neuron>();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                Neurons.Add(new Neuron(numberOfInputs, activationFunction));
            }
        }

        public List<Neuron> Neurons { get; set; }

        public double[] Output { get; set; }

        public double[] Activate(double[] inputs)
        {
            List<double> outputs = new List<double>();
            for (int i = 0; i < Neurons.Count(); i++)
            {
                outputs.Add(Neurons[i].Activate(inputs));
            }
            Output = outputs.ToArray();
            return outputs.ToArray();
        }
    }
}
