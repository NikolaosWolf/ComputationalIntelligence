using ComputationalIntelligence.Core.Models.Enums;
using ComputationalIntelligence.DataSets;
using ComputationalIntelligence.Exercises.Interfaces;
using System;

namespace ComputationalIntelligence.Exercises
{
    public class Exercise1 : IExercise
    {
        private readonly int d = 10; // Number of Input
        private readonly int K = 10; // Number of Categories
        private readonly int H1 = 3; // Number of Neurons of first hidden layer
        private readonly int H2 = 3; // Number of Neurons of second hidden layer
        private readonly int H3 = 3; // Number of Neurons of third hidden layer
        private readonly ActivationFunction F = ActivationFunction.Tanh; // Type of actication function

        public void Execute()
        {
            var eg = new ExampleGenerator();
            var examples = eg.Generate();

            foreach (var example in examples.LearningSet)
                Console.WriteLine($"[{example.X1},{example.X2}]");

            Console.WriteLine($"{examples.LearningSet.Count}");
        }
    }
}
