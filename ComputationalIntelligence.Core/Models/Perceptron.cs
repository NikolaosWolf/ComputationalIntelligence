using ComputationalIntelligence.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputationalIntelligence.Core.Models
{
    public class Perceptron
    {
        private readonly ActivationFunction _activationFunction;

        public Perceptron(int[] numberOfNeuronsPerLayer, ActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;

            Layers = new List<Layer>();

            for (int i = 0; i < numberOfNeuronsPerLayer.Length; i++)
            {
                Layers.Add(new Layer(numberOfNeuronsPerLayer[i], i == 0 ? numberOfNeuronsPerLayer[i] : numberOfNeuronsPerLayer[i - 1], activationFunction));
            }
        }

        public List<Layer> Layers { get; set; }

        public double[] Activate(double[] inputs)
        {
            double[] outputs = new double[0];
            for (int i = 1; i < Layers.Count; i++)
            {
                outputs = Layers[i].Activate(inputs);
                inputs = outputs;
            }
            return outputs;
        }

        double IndividualError(double[] realOutput, double[] desiredOutput)
        {
            double err = 0;
            for (int i = 0; i < realOutput.Length; i++)
            {
                err += Math.Pow(realOutput[i] - desiredOutput[i], 2);
            }
            return err;
        }
        double GeneralError(List<double[]> input, List<double[]> desiredOutput)
        {
            double err = 0;
            for (int i = 0; i < input.Count; i++)
            {
                err += IndividualError(Activate(input[i]), desiredOutput[i]);
            }
            return err;
        }

        public bool Learn(List<double[]> input, List<double[]> desiredOutput, double alpha, double maxError, int maxIterations, String net_path = null, int iter_save = 1)
        {
            double err = 99999;
            int it = maxIterations;
            while (err > maxError)
            {
                ApplyBackPropagation(input, desiredOutput, alpha);
                err = GeneralError(input, desiredOutput);

                maxIterations--;
            }
            
            return true;
        }
        List<double[]> sigmas;
        List<double[,]> deltas;

        void SetSigmas(double[] desiredOutput)
        {
            sigmas = new List<double[]>();
            for (int i = 0; i < Layers.Count; i++)
            {
                sigmas.Add(new double[Layers[i].Neurons.Count()]);
            }
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < Layers[i].Neurons.Count(); j++)
                {
                    if (i == Layers.Count - 1)
                    {
                        double y = Layers[i].Neurons[j].LastActivation;
                        sigmas[i][j] = (Neuron.ActivationFunction(y, _activationFunction) - desiredOutput[j]) * Neuron.SigmoidDerivated(y, _activationFunction);
                    }
                    else
                    {
                        double sum = 0;
                        for (int k = 0; k < Layers[i + 1].Neurons.Count(); k++)
                        {
                            sum += Layers[i + 1].Neurons[k].Weights[j] * sigmas[i + 1][k];
                        }
                        sigmas[i][j] = Neuron.SigmoidDerivated(Layers[i].Neurons[j].LastActivation, _activationFunction) * sum;
                    }
                }
            }
        }
        void SetDeltas()
        {
            deltas = new List<double[,]>();
            for (int i = 0; i < Layers.Count; i++)
            {
                deltas.Add(new double[Layers[i].Neurons.Count(), Layers[i].Neurons[0].Weights.Length]);
            }
        }
        void AddDelta()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                for (int j = 0; j < Layers[i].Neurons.Count(); j++)
                {
                    for (int k = 0; k < Layers[i].Neurons[j].Weights.Length; k++)
                    {
                        deltas[i][j, k] += sigmas[i][j] * Neuron.ActivationFunction(Layers[i - 1].Neurons[k].LastActivation, _activationFunction);
                    }
                }
            }
        }
        void UpdateBias(double alpha)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                for (int j = 0; j < Layers[i].Neurons.Count(); j++)
                {
                    Layers[i].Neurons[j].Bias -= alpha * sigmas[i][j];
                }
            }
        }
        void UpdateWeights(double alpha)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                for (int j = 0; j < Layers[i].Neurons.Count(); j++)
                {
                    for (int k = 0; k < Layers[i].Neurons[j].Weights.Length; k++)
                    {
                        Layers[i].Neurons[j].Weights[k] -= alpha * deltas[i][j, k];
                    }
                }
            }
        }
        void ApplyBackPropagation(List<double[]> input, List<double[]> desiredOutput, double alpha)
        {
            SetDeltas();
            for (int i = 0; i < input.Count; i++)
            {
                Activate(input[i]);
                SetSigmas(desiredOutput[i]);
                UpdateBias(alpha);
                AddDelta();
            }
            UpdateWeights(alpha);

        }
    }
}
