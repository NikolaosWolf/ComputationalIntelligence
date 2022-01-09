using ComputationalIntelligence.Core.Models.Enums;

namespace ComputationalIntelligence.Exercises
{
    public static class Settings1
    {
        public const int d = 10; // Number of Input
        public const int K = 10; // Number of Categories
        public const int H1 = 3; // Number of Neurons of first hidden layer
        public const int H2 = 3; // Number of Neurons of second hidden layer
        public const int H3 = 3; // Number of Neurons of third hidden layer
        public const ActivationFunction F = ActivationFunction.Tanh; // Type of actication function
    }
}
