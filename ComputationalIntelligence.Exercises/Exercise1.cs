using ComputationalIntelligence.Core.Models;
using ComputationalIntelligence.Core.Models.Enums;
using ComputationalIntelligence.DataSets;
using ComputationalIntelligence.DataSets.Models;
using ComputationalIntelligence.Exercises.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputationalIntelligence.Exercises
{
    public class Exercise1 : IExercise
    {
        public void Execute()
        {
            var examples = CreateExamples();

            LoadDefaultSettings();
            ExecuteProgram1(examples.TrainingSet, examples.TestSet);
            ExecuteProgram2(examples.TrainingSet, examples.TestSet);
        }

        private void LoadDefaultSettings()
        {
            Console.WriteLine("The default settings of the Exercise are:");
            Console.WriteLine($"- Number of inputs (d): {Settings1.d}");
            Console.WriteLine($"- Number of categories (K): {Settings1.K}");
            Console.WriteLine($"- Number of neurons of the first hidden layer (H1): {Settings1.H1}");
            Console.WriteLine($"- Number of neurons of the second hidden layer (H2): {Settings1.H2}");
            Console.WriteLine($"- Number of neurons of the third hidden layer (H3): {Settings1.H3}");
            Console.WriteLine($"- Type of activation function for hidden layers: {(Settings1.F == ActivationFunction.Tanh ? "Tanh" : "ReLU")}");
        }

        private ExampleResult CreateExamples()
        {
            var generator = new ExampleGenerator();
            var examples = generator.Generate();

            var noiseGenerator = new NoiseGenerator();
            noiseGenerator.GenerateNoise(examples.TrainingSet);

            return examples;
        }

        private void ExecuteProgram1(ISet<Example> trainingSet, ISet<Example> testSet)
        {
            int[] layers = { Settings1.d, Settings1.H1, Settings1.H2, Settings1.K };

            double learningRate = 0.3;
            double maxError = 0.0001;
            int epochs = 700;

            var perceptron = new Perceptron(layers, Settings1.F);

            while (!perceptron.Learn(trainingSet.Select(s => new double[] { s.X1, s.X2}).ToList(), 
                                     trainingSet.Select(s => new double[] { (double)s.Category}).ToList(),
                                     learningRate,
                                     maxError,
                                     epochs))
            {
                perceptron = new Perceptron(layers, Settings1.F);
            }
        }

        private void ExecuteProgram2(ISet<Example> trainingSet, ISet<Example> testSet)
        {
            int[] layers = { Settings1.d, Settings1.H1, Settings1.H2, Settings1.H3, Settings1.K };

            double learningRate = 0.3;
            double maxError = 0.0001;
            int epochs = 700;

            var perceptron = new Perceptron(layers, Settings1.F);

            while (!perceptron.Learn(trainingSet.Select(s => new double[] { s.X1, s.X2 }).ToList(),
                                     trainingSet.Select(s => new double[] { (double)s.Category }).ToList(),
                                     learningRate,
                                     maxError,
                                     epochs))
            {
                perceptron = new Perceptron(layers, Settings1.F);
            }
        }
    }
}
