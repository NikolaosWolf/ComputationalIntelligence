using ComputationalIntelligence.Core.Extras;
using ComputationalIntelligence.Core.Models.Enums;
using System;

namespace ComputationalIntelligence.Core.Models
{
    public class Neuron
    {
        private readonly ActivationFunction _activationFunction;

        public Neuron(int numberOfInputs, ActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;

            Weights = new double[numberOfInputs];

            var rnd = new Random();
            for (int i = 0; i < numberOfInputs; i++)
            {
                Weights[i] = 10 * rnd.NextDouble() - 5;
            }
        }

        public double[] Weights { get; set; }

        public double LastActivation { get; set; }

        public double Bias { get; set; }

        public double Activate(double[] inputs)
        {
            double activation = Bias;

            for (int i = 0; i < Weights.Length; i++)
            {
                activation += Weights[i] * inputs[i];
            }

            LastActivation = activation;
            return ActivationFunction(activation, _activationFunction);
        }

        public static double ActivationFunction(double input, ActivationFunction activationFunction)
        {
            switch (activationFunction)
            {
                case Enums.ActivationFunction.Tanh:
                    return Functions.Tanh(input, 1);
                case Enums.ActivationFunction.ReLU:
                    return Functions.ReLU(input);
                default:
                    return 0;
            }
        }

        public static double SigmoidDerivated(double input, ActivationFunction activationFunction)
        {
            double y = ActivationFunction(input, activationFunction);
            return y * (1 - y);
        }
    }
}
