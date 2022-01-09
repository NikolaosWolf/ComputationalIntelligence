using System.Collections.Generic;

namespace ComputationalIntelligence.Core.Models
{
    public class Perceptron
    {
        public Perceptron(int[] numberOfNeuronsPerLayer)
        {
            Layers = new List<Layer>();

            for (int i = 0; i < numberOfNeuronsPerLayer.Length; i++)
            {
                Layers.Add(new Layer(numberOfNeuronsPerLayer[i], i == 0 ? numberOfNeuronsPerLayer[i] : numberOfNeuronsPerLayer[i - 1]));
            }
        }

        public List<Layer> Layers { get; set; }
    }
}
